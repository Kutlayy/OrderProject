using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;
using Acme.OrderProject.Orders;

namespace Acme.OrderProject.Orders
{
    public class OrderAppService : ApplicationService, IOrderAppService
    {
        private readonly IRepository<Order, Guid> _orderRepo;
        private readonly IRepository<OrderLine, Guid> _orderLineRepo;
        private readonly OrderManager _orderManager;

        public OrderAppService(
            IRepository<Order, Guid> orderRepo,
            IRepository<OrderLine, Guid> orderLineRepo,
            OrderManager orderManager)
        {
            _orderRepo = orderRepo;
            _orderLineRepo = orderLineRepo;
            _orderManager = orderManager;
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
            var order = await _orderManager.CreateAsync(
                input.CustomerId,
                input.OrderDate,
                input.DeliveryAddress);

            await _orderRepo.InsertAsync(order, autoSave: true);
            return ObjectMapper.Map<Order, OrderDto>(order);
        }

        // Siparişe satır ekleme (limit ve stok kontrolü)
        public async Task<OrderDto> AddLineAsync(AddOrderLineDto input)
        {
            var order = await _orderRepo.GetAsync(input.OrderId, includeDetails: true);

            var line = await _orderManager.AddLineAsync(order, input.StockId, input.Quantity);

            await _orderLineRepo.InsertAsync(line, autoSave: true);
            await _orderRepo.UpdateAsync(order, autoSave: true);

            order.Lines = await _orderLineRepo.GetListAsync(l => l.OrderId == order.Id);
            return ObjectMapper.Map<Order, OrderDto>(order);
        }

        // Siparişi onaylama
        public async Task ApproveAsync(Guid orderId)
        {
            var order = await _orderRepo.GetAsync(orderId, includeDetails: true);
            var lines = await _orderLineRepo.GetListAsync(l => l.OrderId == order.Id);

            await _orderManager.ApproveAsync(order, lines);

            await _orderRepo.UpdateAsync(order, autoSave: true);
        }

        // Sipariş silme
        public async Task DeleteAsync(Guid id)
        {
            var order = await _orderRepo.GetAsync(id);
            var lines = await _orderLineRepo.GetListAsync(l => l.OrderId == id);

            _orderManager.CheckDeleteAsync(order);

            foreach (var line in lines)
            {
                await _orderLineRepo.DeleteAsync(line, autoSave: true);
            }

            await _orderRepo.DeleteAsync(order);
        }
    }
}
