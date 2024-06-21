using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using Database_Layer.DatabaseEntities;


namespace Logic_Layer.ViewModels
{
    public class OrderSummaryViewModel
    {
        public int UserId { get; set; }
        public List<ProductViewModel> Products { get; set; }
        public decimal TotalAmount { get; set; }
        public int Quantity { get; set; }
    }
}
