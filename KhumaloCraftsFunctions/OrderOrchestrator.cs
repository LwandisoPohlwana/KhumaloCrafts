using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;
using Logic_Layer.ViewModels;
using KhumaloCraftsFunctions.Models;
using Microsoft.Azure.WebJobs.Extensions.DurableTask;

namespace KhumaloCraftsFunctions
{
    public static class OrderOrchestrator
    {
        [FunctionName("OrderOrchestrator")]
        public static async Task<List<string>> RunOrchestrator(
            [OrchestrationTrigger] IDurableOrchestrationContext context, ILogger log)
        {
            var orderData = context.GetInput<OrderData>();

            // Step 1: Update inventory
            await context.CallActivityAsync("UpdateInventoryActivity", orderData);

            // Step 2: Process payment
            var paymentResult = await context.CallActivityAsync<bool>("ProcessPaymentActivity", orderData);

            // Step 3: Send order confirmation
            await context.CallActivityAsync("SendOrderConfirmationActivity", orderData);

            // Collect and return notification details
            var notificationDetails = new List<string>
            {
                $"Inventory updated for order ID: {orderData.OrderId}",
                $"Payment processed for order ID: {orderData.OrderId}",
                $"Order confirmation sent for order ID: {orderData.OrderId}"
            };

            return notificationDetails;
        }
    }
}
