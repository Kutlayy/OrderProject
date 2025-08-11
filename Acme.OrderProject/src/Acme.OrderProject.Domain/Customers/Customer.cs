using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Entities.Auditing;

namespace Acme.OrderProject.Customers
{
    public class Customer : AuditedAggregateRoot<Guid>
    {
        public string Name { get; set; }

        public decimal RiskLimit { get; set; }

        public string BillAddress { get; set; }
        public Customer(Guid id) : base(id) { }

    }
}
