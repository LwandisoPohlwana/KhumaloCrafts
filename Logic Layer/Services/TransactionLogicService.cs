using Database_Layer.DatabaseEntities;
using Database_Layer.DataServices;
using Logic_Layer.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic_Layer.Services
{
    public class TransactionLogicService : ITransactionLogicService
    {
        private readonly ApplicationDbContext _context;

        public TransactionLogicService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<Transaction>> GetTransactionsByUserIdAsync(int userId)
        {
            return await _context.Transactions
                .Where(t => t.UserId == userId)
                .ToListAsync();
        }

        public async Task CreateTransactionAsync(int userId, List<int> productIds, int quantity, decimal totalAmount, DateTime transactionDate)
        {
            var user = await _context.Users.FindAsync(userId);
            if (user == null)
            {
                throw new ArgumentException("User not found.");
            }

            var products = await _context.Products.Where(p => productIds.Contains(p.ProductId)).ToListAsync();
            if (products.Count != productIds.Count)
            {
                throw new ArgumentException("One or more products not found.");
            }

            totalAmount = 0;
            quantity = 0;

            var transaction = new Transaction
            {
                UserId = userId,
                TransactionDate = transactionDate,
                Products = new List<Product>()
            };

            foreach (var product in products)
            {
                totalAmount += product.Price;
                quantity++;
                transaction.Products.Add(product);
            }

            transaction.TotalAmount = totalAmount;
            transaction.Quantity = quantity;

            _context.Transactions.Add(transaction);
            await _context.SaveChangesAsync();
        }

        public async Task<List<Product>> GetProductsByIdsAsync(List<int> productIds)
        {
            return await _context.Products
                .Where(p => productIds.Contains(p.ProductId))
                .ToListAsync();
        }

        public async Task AddTransactionAsync(Transaction transaction)
        {
            _context.Transactions.Add(transaction);
            await _context.SaveChangesAsync();
        }

        public async Task<List<Transaction>> GetAllTransactionsAsync()
        {
            return await _context.Transactions.ToListAsync();
        }
        public async Task<User> GetUserByEmailAsync(string email)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
        }
    }
}
