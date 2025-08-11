using Acme.OrderProject.Customers;
using Acme.OrderProject.Stocks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;    
using Volo.Abp.Domain.Repositories;
using Acme.OrderProject.Orders;
using Volo.Abp.Guids;




namespace Acme.OrderProject.Orders
{
    public class OrderAppService : ApplicationService, IOrderAppService
    {
        private readonly IRepository<Order, Guid> _orderRepo;
        private readonly IRepository<OrderLine, Guid> _orderLineRepo;
        private readonly IRepository<Stock, Guid> _stockRepo;
        private readonly IRepository<Customer, Guid> _customerRepo;

        public OrderAppService(
        IRepository<Order, Guid> orderRepo,
        IRepository<OrderLine, Guid> orderLineRepo,
        IRepository<Stock, Guid> stockRepo,
        IRepository<Customer, Guid> customerRepo)
        {
            _orderRepo = orderRepo;
            _orderLineRepo = orderLineRepo;
            _stockRepo = stockRepo;
            _customerRepo = customerRepo;
        }
        public async Task<OrderDto> GetAsync(Guid id)
        {
            var order = await _orderRepo.GetAsync(id, includeDetails: true);
            order.Lines = await _orderLineRepo.GetListAsync(l => l.OrderId == id);
            return ObjectMapper.Map<Order, OrderDto>(order);
        }
        public async Task<PagedResultDto<OrderDto>> GetListAsync(PagedAndSortedResultRequestDto input)
        {
            var orders = await _orderRepo.GetPagedListAsync(
                input.SkipCount,
                input.MaxResultCount,
                input.Sorting ?? nameof(Order.CreationTime)
            );

            var totalCount = await _orderRepo.GetCountAsync();

            foreach (var order in orders)
            {
                order.Lines = await _orderLineRepo.GetListAsync(l => l.OrderId == order.Id);
            }

            return new PagedResultDto<OrderDto>(
                totalCount,
                ObjectMapper.Map<List<Order>, List<OrderDto>>(orders)
            );
        }

        // Sipariş oluşturma (Müşteri mevcut olmalı)
        public async Task<OrderDto> CreateAsync(CreateOrderDto input)
        {
            await _customerRepo.GetAsync(input.CustomerId); // Müşteri kontrolü

            var order = ObjectMapper.Map<CreateOrderDto, Order>(input);
            order.IsApproved = false;
            order.TotalAmount = 0m;

            await _orderRepo.InsertAsync(order, autoSave: true);
            return ObjectMapper.Map<Order, OrderDto>(order);
        }

        // Siparişe satır ekleme (limit ve stok kontrolü)
        public async Task<OrderDto> AddLineAsync(AddOrderLineDto input)
        {
            var order = await _orderRepo.GetAsync(input.OrderId, includeDetails: true);
            if (order.IsApproved)
                throw new BusinessException("Approved orders cannot be modified.");

            var stock = await _stockRepo.GetAsync(input.StockId);
            if (stock.Quantity < input.Quantity)
                throw new BusinessException("Insufficient stock.");

            var customer = await _customerRepo.GetAsync(order.CustomerId);
            var lineTotal = stock.Price * input.Quantity;
                
            if (customer.RiskLimit < order.TotalAmount + lineTotal)
                throw new BusinessException("Customer risk limit exceeded.");

            var line = new OrderLine(Guid.NewGuid())   // veya GuidGenerator.Create()
            {
                OrderId = order.Id,
                StockId = stock.Id,
                Quantity = input.Quantity,
                LineTotal = lineTotal
            };

            await _orderLineRepo.InsertAsync(line, autoSave: true);

            order.TotalAmount += lineTotal;
            await _orderRepo.UpdateAsync(order, autoSave: true);

            order.Lines = await _orderLineRepo.GetListAsync(l => l.OrderId == order.Id);
            return ObjectMapper.Map<Order, OrderDto>(order);
        }

        // Siparişi onaylama
        public async Task ApproveAsync(Guid orderId)
        {
            var order = await _orderRepo.GetAsync(orderId, includeDetails: true);
            if (order.IsApproved)
                return;

            var lines = await _orderLineRepo.GetListAsync(l => l.OrderId == order.Id);
            if (lines.Count == 0)
                throw new BusinessException("Order has no lines.");

            var customer = await _customerRepo.GetAsync(order.CustomerId);
            if (customer.RiskLimit < order.TotalAmount)
                throw new BusinessException("Customer risk limit exceeded.");

            foreach (var line in lines)
            {
                var stock = await _stockRepo.GetAsync(line.StockId);
                if (stock.Quantity < line.Quantity)
                    throw new BusinessException("Insufficient stock.");
            }

            // Risk limit ve stok düş
            customer.RiskLimit -= order.TotalAmount;
            await _customerRepo.UpdateAsync(customer, autoSave: true);

            foreach (var line in lines)
            {
                var stock = await _stockRepo.GetAsync(line.StockId);
                stock.Quantity -= line.Quantity;
                await _stockRepo.UpdateAsync(stock, autoSave: true);
            }

            order.IsApproved = true;
            await _orderRepo.UpdateAsync(order, autoSave: true);
        }

        // Sipariş silme
        public async Task DeleteAsync(Guid id)
        {
            var order = await _orderRepo.GetAsync(id);
            if (order.IsApproved)
                throw new BusinessException("Approved orders cannot be deleted.");

            var lines = await _orderLineRepo.GetListAsync(l => l.OrderId == id);
            foreach (var line in lines)
            {
                await _orderLineRepo.DeleteAsync(line, autoSave: true);
            }

            await _orderRepo.DeleteAsync(order);
        }
    }
}
    

