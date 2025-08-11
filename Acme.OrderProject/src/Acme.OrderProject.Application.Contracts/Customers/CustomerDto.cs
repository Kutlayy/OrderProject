using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;

namespace Acme.OrderProject.Customers
{
    public class CustomerDto : AuditedEntityDto<Guid>
    {
        public string Name { get; set; }

        public decimal RiskLimit { get; set; }

        public string BillAddress { get; set; }

    }
}
