using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Logic_Layer.Interfaces;
using Logic_Layer.ViewModels;
using Microsoft.Azure.WebJobs.Extensions.DurableTask;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

public class DurableFunctionsService : IDurableFunctionsService
{
    private readonly IDurableOrchestrationClient _durableClient;
    private readonly HttpClient _httpClient;

    public DurableFunctionsService(IDurableOrchestrationClient durableClient, HttpClient httpClient)
    {
        _durableClient = durableClient;
        _httpClient = httpClient;
    }

    public async Task<List<string>> GetNotificationDetailsAsync(string instanceId)
    {
        var orchestrationState = await _durableClient.GetStatusAsync(instanceId);

        if (orchestrationState?.RuntimeStatus != OrchestrationRuntimeStatus.Completed)
        {
            throw new Exception($"Orchestration with instance ID '{instanceId}' did not complete successfully.");
        }

        var outputToken = orchestrationState.Output as JToken;
        if (outputToken == null)
        {
            throw new Exception($"No output found for orchestration with instance ID '{instanceId}'.");
        }

        var notificationDetails = outputToken.ToObject<List<string>>();
        return notificationDetails;
    }

    public async Task<List<NotificationModel>> GetNotificationsAsync(string instanceId)
    {
        try
        {
            string functionUrl = $"http://your-function-app.azurewebsites.net/api/GetNotifications/{instanceId}";

            HttpResponseMessage response = await _httpClient.GetAsync(functionUrl);

            if (response.IsSuccessStatusCode)
            {
                string jsonContent = await response.Content.ReadAsStringAsync();
                List<NotificationModel> notifications = JsonConvert.DeserializeObject<List<NotificationModel>>(jsonContent);
                return notifications;
            }
            else
            {
                Console.WriteLine($"Failed to fetch notifications. Status code: {response.StatusCode}");
                return new List<NotificationModel>();
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Exception while fetching notifications: {ex.Message}");
            return new List<NotificationModel>();
        }
    }
}
