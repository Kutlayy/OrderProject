using System;
using Volo.Abp.Domain.Entities;

namespace Acme.OrderProject.Orders
{
    public class OrderLine : Entity<Guid>
    {
        protected OrderLine() { }

        public OrderLine(Guid id) : base(id) { }

        public Guid OrderId { get; set; }
        public Guid StockId { get; set; }
        public int Quantity { get; set; }   // sende int ise int bırak
        public decimal LineTotal { get; set; }
    }
}
