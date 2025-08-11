using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Entities.Auditing;

namespace Acme.OrderProject.Stocks
{
    public class Stock : AuditedAggregateRoot<Guid>
    {
        public string Name { get; set; }
        public decimal Quantity { get; set; }
        public decimal Price { get; set; }
        public Stock(Guid id) : base(id) { }
        public Stock() { }

        public Stock(Guid id, string name, int quantity, decimal price) : base(id)
        {
            Name = name;
            Quantity = quantity;
            Price = price;
        }





    }
}
