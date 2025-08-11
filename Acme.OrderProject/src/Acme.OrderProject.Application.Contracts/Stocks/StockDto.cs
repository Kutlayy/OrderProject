using System;
using Volo.Abp.Application.Dtos;

namespace Acme.OrderProject.Stocks
{
    public class StockDto : EntityDto<Guid>  // <-- BURASI ÖNEMLİ
    {
        public string Name { get; set; }
        public decimal Quantity { get; set; } // DbContext'le uyumluysa decimal
        public decimal Price { get; set; }
    }
}
