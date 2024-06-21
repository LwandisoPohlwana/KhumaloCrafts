using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database_Layer.DatabaseEntities
{
    public class TransactionDetail
    {
        public int TransactionDetailId { get; set; }
        public Transaction Transaction { get; set; }
        public int TransactionId { get; set; }
        public int ProductId { get; set; }
        public Product Product { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }   
    }
}
