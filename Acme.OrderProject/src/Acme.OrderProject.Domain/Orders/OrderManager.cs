using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Domain.Services;
using Volo.Abp.Guids;
using Acme.OrderProject.Customers;
using Acme.OrderProject.Stocks;

namespace Acme.OrderProject.Orders;

public class OrderManager : DomainService
{
    private readonly IStockRepository _stockRepository;
    private readonly ICustomerRepository _customerRepository;
    private readonly IGuidGenerator _guidGenerator;

    public OrderManager(
        IStockRepository stockRepository,
        ICustomerRepository customerRepository,
        IGuidGenerator guidGenerator)
    {
        _stockRepository = stockRepository;
        _customerRepository = customerRepository;
        _guidGenerator = guidGenerator;
    }

    public async Task<Order> CreateAsync(Guid customerId, DateTime orderDate, string deliveryAddress)
    {
        await _customerRepository.GetAsync(customerId);

        return new Order(_guidGenerator.Create())
        {
            CustomerId = customerId,
            OrderDate = orderDate,
            DeliveryAddress = deliveryAddress,
            IsApproved = false,
            TotalAmount = 0m
        };


    }

    public async Task<OrderLine> AddLineAsync(Order order, Guid stockId, int quantity)
    {
        if (order.IsApproved)
        {
            throw new BusinessException("Approved orders cannot be modified.");
        }

        var stock = await _stockRepository.GetAsync(stockId);
        if (stock.Quantity < quantity)
        {
            throw new BusinessException("Insufficient stock.");
        }

        var customer = await _customerRepository.GetAsync(order.CustomerId);
        var lineTotal = stock.Price * quantity;
        if (customer.RiskLimit < order.TotalAmount + lineTotal)
        {
            throw new BusinessException("Customer risk limit exceeded.");
        }

        order.TotalAmount += lineTotal;

        return new OrderLine(_guidGenerator.Create())
        {
            OrderId = order.Id,
            StockId = stock.Id,
            Quantity = quantity,
            LineTotal = lineTotal
        };
    }

    public async Task ApproveAsync(Order order, List<OrderLine> lines)
    {
        if (order.IsApproved)
        {
            return;
        }

        if (lines.Count == 0)
        {
            throw new BusinessException("Order has no lines.");
        }

        var customer = await _customerRepository.GetAsync(order.CustomerId);
        if (customer.RiskLimit < order.TotalAmount)
        {
            throw new BusinessException("Customer risk limit exceeded.");
        }

        foreach (var line in lines)
        {
            var stock = await _stockRepository.GetAsync(line.StockId);
            if (stock.Quantity < line.Quantity)
            {
                throw new BusinessException("Insufficient stock.");
            }
        }

        customer.RiskLimit -= order.TotalAmount;
        await _customerRepository.UpdateAsync(customer, autoSave: true);

        foreach (var line in lines)
        {
            var stock = await _stockRepository.GetAsync(line.StockId);
            stock.Quantity -= line.Quantity;
            await _stockRepository.UpdateAsync(stock, autoSave: true);
        }

        order.IsApproved = true;
    }

    public void CheckDeleteAsync(Order order)
    {
        if (order.IsApproved)
        {
            throw new BusinessException("Approved orders cannot be deleted.");
        }
    }
}
