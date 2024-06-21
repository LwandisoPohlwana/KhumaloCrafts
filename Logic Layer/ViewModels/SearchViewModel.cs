using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic_Layer.ViewModels
{
    public class SearchViewModel
    {
        public string SearchTerm { get; set; }
        public List<ProductViewModel> Products { get; set; }
    }
}
