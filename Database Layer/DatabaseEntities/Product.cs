using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database_Layer.DatabaseEntities
{
    public class Product
    {
        public int ProductId { get; set; }
        public string Title { get; set; }
        public string ArtForm { get; set; }
        public string Artist { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public string ImageUrl { get; set; }
        public bool Available { get; set; }
        public int StockQuantity { get; set; }  // New property for stock quantity
        public ICollection<Transaction> Transactions { get; set; }
        public ICollection<TransactionDetail> TransactionDetails { get; set; }
    }
}

