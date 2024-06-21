using Logic_Layer.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic_Layer.Interfaces
{
    public interface IInventoryService
    {
        Task<string> UpdateInventoryAsync(ProductViewModel product);
    }
}
