using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KhumaloCraftsFunctions.Models
{
    public class OrderData
    {
    public int OrderId { get; set; }
    public int UserId { get; set; }
    public decimal TotalAmount { get; set; }
    public List<ProductData> Products { get; set; }
    }
}
