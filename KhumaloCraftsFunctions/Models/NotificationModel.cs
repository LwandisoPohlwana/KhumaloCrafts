using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KhumaloCraftsFunctions.Models
{
    public class NotificationModel
    {
        public string NotificationId { get; set; }
        public string Title { get; set; }
        public string Message { get; set; }
        public DateTime Timestamp { get; set; }
    }
}
