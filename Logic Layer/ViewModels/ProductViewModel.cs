using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic_Layer.ViewModels
{
    public class ProductViewModel
    {
        public int ProductId { get; set; }
        public string Title { get; set; }
        public decimal Price { get; set; }
        public string Description { get; set; }
        public string ArtForm { get; set; }
        public int Quantity { get; set; }
    }
}
