using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic_Layer.ViewModels
{
    public class OrderDataViewModel
    {
        public int OrderId { get; set; }
        public int UserId { get; set; }
        public decimal TotalAmount { get; set; }
        public List<ProductViewModel> Products { get; set; }
        public string CustomerEmail { get; set; }
    }
}
