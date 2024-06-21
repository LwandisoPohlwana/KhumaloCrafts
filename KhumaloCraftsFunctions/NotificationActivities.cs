using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.DurableTask;
using Microsoft.Extensions.Logging;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using KhumaloCraftsFunctions.Models;
using Newtonsoft.Json;
using System;

public static class NotificationActivities
{
    [FunctionName("SendOrderReceivedNotification")]
    public static async Task SendOrderReceivedNotification([ActivityTrigger] OrderData orderData, ILogger log)
    {
        log.LogInformation($"Order received for OrderId: {orderData.OrderId}, UserId: {orderData.UserId}");
        await SendPushNotification("Order Received", $"Your order with ID {orderData.OrderId} has been received.");
    }

    [FunctionName("SendPaymentSuccessfulNotification")]
    public static async Task SendPaymentSuccessfulNotification([ActivityTrigger] OrderData orderData, ILogger log)
    {
        log.LogInformation($"Payment successful for OrderId: {orderData.OrderId}, UserId: {orderData.UserId}");
        await SendPushNotification("Payment Successful", $"Payment for your order with ID {orderData.OrderId} was successful.");
    }

    [FunctionName("SendOrderConfirmedNotification")]
    public static async Task SendOrderConfirmedNotification([ActivityTrigger] OrderData orderData, ILogger log)
    {
        log.LogInformation($"Order confirmed for OrderId: {orderData.OrderId}, UserId: {orderData.UserId}");
        await SendPushNotification("Order Confirmed", $"Your order with ID {orderData.OrderId} has been confirmed.");
    }

    private static async Task SendPushNotification(string title, string message)
    {
        using (var httpClient = new HttpClient())
        {
            var notificationContent = new
            {
                title = title,
                message = message
            };

            var jsonContent = JsonConvert.SerializeObject(notificationContent);
            var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

            var response = await httpClient.PostAsync("http://your-frontend-url/api/notification", content);

            if (!response.IsSuccessStatusCode)
            {
                // Handle the error appropriately in a real scenario
                throw new Exception("Failed to send notification.");
            }
        }
    }
}
