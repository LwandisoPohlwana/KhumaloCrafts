using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace  Database_Layer.DatabaseEntities
{
    public class Transaction
    {
        public int TransactionId { get; set; }
        public DateTime TransactionDate { get; set; }
        public decimal TotalAmount { get; set; }

        public int UserId { get; set; }
        public User User { get; set; }

        public ICollection<TransactionDetail> TransactionDetails { get; set; }
        public ICollection<Product> Products { get; set; }
        public int Quantity { get; set; } // Define Quantity property
    }

}
