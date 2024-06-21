using Logic_Layer.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic_Layer.Interfaces
{
    public interface IDurableFunctionsService
    {
        Task<List<string>> GetNotificationDetailsAsync(string instanceId);
        Task<List<NotificationModel>> GetNotificationsAsync(string instanceId);
    }
}
