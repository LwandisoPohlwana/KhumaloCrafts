using Microsoft.Azure.WebJobs.Extensions.DurableTask;
using Microsoft.Azure.WebJobs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KhumaloCraftsFunctions.Models;
using Microsoft.Extensions.Logging;
using Database_Layer.DataServices;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace KhumaloCraftsFunctions
{
    public class ActivityFunctions
    {
      
        private readonly ApplicationDbContext _context;

        public ActivityFunctions(ApplicationDbContext context)
        {
            _context = context;
        }

        [FunctionName("UpdateInventoryActivity")]
        public async Task UpdateInventory([ActivityTrigger] OrderData orderData)
        {
            var productIds = orderData.Products.Select(p => p.ProductId).ToList();

            var products = await _context.Products.Where(p => productIds.Contains(p.ProductId)).ToListAsync();

            foreach (var product in products)
            {
                var purchasedQuantity = orderData.Products.First(p => p.ProductId == product.ProductId).Quantity;
                product.StockQuantity -= purchasedQuantity;
                if (product.StockQuantity < 0)
                {
                    product.StockQuantity = 0;
                }
            }

            await _context.SaveChangesAsync();
        }

        [FunctionName("ProcessPaymentActivity")]
        public static async Task<bool> ProcessPayment([ActivityTrigger] OrderData orderData)
        {
            // Implement your logic to process payment here
            Console.WriteLine("Processing payment...");
            await Task.Delay(1000); // Simulate payment processing
            Console.WriteLine("Payment processed successfully.");
            return true; // Return true for successful payment, false for failed payment
        }

        [FunctionName("SendOrderConfirmationActivity")]
        public static async Task SendOrderConfirmation([ActivityTrigger] OrderData orderData)
        {
            // Implement your logic to send order confirmation here
            Console.WriteLine("Sending order confirmation...");
            await Task.Delay(1000); // Simulate sending confirmation
            Console.WriteLine("Order confirmation sent.");
        }
    }
}
