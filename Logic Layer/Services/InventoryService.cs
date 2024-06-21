using Database_Layer.DataServices;
using Logic_Layer.Interfaces;
using Logic_Layer.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic_Layer.Services
{
    public class InventoryService : IInventoryService
    {
        private readonly ApplicationDbContext _context;

        public InventoryService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<string> UpdateInventoryAsync(ProductViewModel product)
        {
            var productEntity = await _context.Products.FindAsync(product.ProductId);
            if (productEntity == null)
            {
                return $"Product with ID {product.ProductId} not found.";
            }

            productEntity.StockQuantity -= product.Quantity;
            if (productEntity.StockQuantity < 0)
            {
                productEntity.StockQuantity = 0;
            }

            await _context.SaveChangesAsync();
            return $"Inventory updated for product {product.ProductId}";
        }
    }
}
