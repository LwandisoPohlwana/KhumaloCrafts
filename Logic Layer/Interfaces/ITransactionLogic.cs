using Database_Layer.DatabaseEntities;
using Logic_Layer.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic_Layer.Interfaces
{
    public interface ITransactionLogic
    {
        Task ConfirmOrderAsync(OrderSummaryViewModel model);
        Task<List<Product>> SearchProductsAsync(string searchTerm);
        Task<List<Transaction>> GetTransactionsByUserIdAsync(int userId);
        Task AddTransactionAsync(Transaction transaction);
        Task AddTransactionDetailsAsync(List<TransactionDetail> transactionDetails);
        Task<List<Transaction>> GetAllTransactionsAsync();
        Task<User> GetUserByEmailAsync(string email);
        Task<List<Product>> GetProductsByIdsAsync(List<int> productIds);
    }

}
