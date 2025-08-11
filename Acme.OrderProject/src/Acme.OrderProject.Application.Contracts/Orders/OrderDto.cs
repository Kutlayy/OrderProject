using System;
using System.Collections.Generic;
using Volo.Abp.Application.Dtos;

namespace Acme.OrderProject.Orders;

public class OrderLineDto : EntityDto<Guid>
{
    public Guid StockId { get; set; }
    public int Quantity { get; set; }
    public decimal LineTotal { get; set; }
}

public class OrderDto : AuditedEntityDto<Guid>
{
    public Guid CustomerId { get; set; }
    public DateTime OrderDate { get; set; }
    public string DeliveryAddress { get; set; }
    public bool IsApproved { get; set; }
    public decimal TotalAmount { get; set; }
    public List<OrderLineDto> Lines { get; set; } = new();
}

public class CreateOrderDto
{
    public Guid CustomerId { get; set; }
    public DateTime OrderDate { get; set; }
    public string DeliveryAddress { get; set; }
}

public class AddOrderLineDto
{
    public Guid OrderId { get; set; }
    public Guid StockId { get; set; }
    public int Quantity { get; set; }
}
