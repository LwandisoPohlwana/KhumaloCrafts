using Database_Layer.DatabaseEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace Logic_Layer.Interfaces
{
    public interface ITransactionLogicService
        {
        Task<List<Transaction>> GetAllTransactionsAsync();
        Task<List<Transaction>> GetTransactionsByUserIdAsync(int userId);
        Task<List<Product>> GetProductsByIdsAsync(List<int> productIds);
        Task AddTransactionAsync(Transaction transaction);
        Task CreateTransactionAsync(int userId, List<int> productIds, int quantity, decimal totalAmount, DateTime transactionDate);
        Task<User> GetUserByEmailAsync(string email);
    }

}
