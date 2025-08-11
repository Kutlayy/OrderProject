using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Acme.OrderProject.Stocks
{
    public class CreateUpdateStockDto
    {
        [Required]
        [StringLength(128)]
        public string Name { get; set; }
        [Required]
        public int Quantity { get; set; }
        [Required]
        public decimal Price { get; set; }

        // Additional properties can be added as needed
    }
}
