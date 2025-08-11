using System;
using System.Collections.Generic;
using Volo.Abp.Domain.Entities.Auditing;

namespace Acme.OrderProject.Orders
{
    public class Order : AuditedAggregateRoot<Guid> // veya Entity<Guid>
    {
        // EF Core için parametresiz ctor şart (protected yeterli)
        protected Order() { }

        // EF'nin Id'ye bağlayabilmesi için parametre adı 'id' olmalı
        public Order(Guid id) : base(id)
        {
            Lines = new List<OrderLine>();
        }

        public Guid CustomerId { get; set; }
        public DateTime OrderDate { get; set; }
        public string DeliveryAddress { get; set; } = default!;
        public decimal TotalAmount { get; set; }
        public bool IsApproved { get; set; }

        public ICollection<OrderLine> Lines { get; set; } = new List<OrderLine>();
    }
}