using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Acme.OrderProject.Customers
{
    public class CreateUpdateCustomerDto
    {
        [Required]
        [StringLength(128)]
        public string Name { get; set; }

        [Required]
        public decimal RiskLimit { get; set; }

        [Required]
        [StringLength(256)]
        public string BillAddress { get; set; }
    }
}
