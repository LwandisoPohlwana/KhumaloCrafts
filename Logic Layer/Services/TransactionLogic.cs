using Database_Layer.DatabaseEntities;
using Database_Layer.DataServices;
using Logic_Layer.Interfaces;
using Logic_Layer.ViewModels;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Text;


namespace Logic_Layer.Services
{
    public class TransactionLogic : ITransactionLogic
    {
        private readonly ApplicationDbContext _context;
        private readonly HttpClient _httpClient;

        public TransactionLogic(ApplicationDbContext context, HttpClient httpClient)
        {
            _context = context;
            _httpClient = httpClient;
        }

        public async Task ConfirmOrderAsync(OrderSummaryViewModel model)
        {
            var productIds = model.Products.Select(p => p.ProductId).ToList();
            var quantities = model.Products.Select(p => 1).ToList(); // Assuming quantity of 1 for simplicity

            var transaction = new Transaction
            {
                UserId = model.UserId,
                TransactionDate = DateTime.Now,
                TotalAmount = model.TotalAmount,
                Products = model.Products.Select(p => new Product
                {
                    ProductId = p.ProductId,
                    Price = p.Price,
                    Title = p.Title,
                    // Add other properties as needed
                }).ToList(),
                Quantity = model.Quantity
            };

            await AddTransactionAsync(transaction);

            // Create TransactionDetails
            var transactionDetails = model.Products.Select(p => new TransactionDetail
            {
                TransactionId = transaction.TransactionId,
                ProductId = p.ProductId,
                Quantity = 1, // Adjust quantity as needed
                Price = p.Price
            }).ToList();

            await AddTransactionDetailsAsync(transactionDetails);

            string instanceId = await StartOrderOrchestratorAsync(model);
            SaveInstanceIdToDatabase(model.UserId, instanceId);
        }

        public async Task<List<Product>> SearchProductsAsync(string searchTerm)
        {
            return await _context.Products
                .Where(p =>
                    EF.Functions.Like(p.ProductId.ToString(), $"%{searchTerm}%") ||
                    EF.Functions.Like(p.Title, $"%{searchTerm}%") ||
                    EF.Functions.Like(p.Description, $"%{searchTerm}%") ||
                    EF.Functions.Like(p.ArtForm, $"%{searchTerm}%"))
                .ToListAsync();
        }

        public async Task<List<Transaction>> GetTransactionsByUserIdAsync(int userId)
        {
            return await _context.Transactions
                .Where(t => t.UserId == userId)
                .ToListAsync();
        }

        public async Task AddTransactionAsync(Transaction transaction)
        {
            _context.Transactions.Add(transaction);
            await _context.SaveChangesAsync();
        }

        public async Task AddTransactionDetailsAsync(List<TransactionDetail> transactionDetails)
        {
            _context.TransactionDetails.AddRange(transactionDetails);
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
        public async Task<List<Product>> GetProductsByIdsAsync(List<int> productIds)
        {
            return await _context.Products
                .Where(p => productIds.Contains(p.ProductId))
                .ToListAsync();
        }
        private ProductViewModel MapToViewModel(Product product)
        {
            return new ProductViewModel
            {
                ProductId = product.ProductId,
                Title = product.Title,
                Price = product.Price,
                // Map other properties as needed
            };
        }

        private async Task<string> StartOrderOrchestratorAsync(OrderSummaryViewModel model)
        {
            var orderData = new OrderDataViewModel
            {
                UserId = model.UserId,
                TotalAmount = model.TotalAmount,
                Products = model.Products
            };

            var jsonContent = new StringContent(JsonConvert.SerializeObject(orderData), Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync("http://your-function-app.azurewebsites.net/api/HttpStart", jsonContent);

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception("Failed to start the order processing workflow.");
            }

            var responseData = await response.Content.ReadAsStringAsync();
            dynamic data = JsonConvert.DeserializeObject(responseData);
            return data.instanceId;
        }

        private void SaveInstanceIdToDatabase(int userId, string instanceId)
        {
            var user = _context.Users.FirstOrDefault(u => u.UserId == userId);
            if (user != null)
            {
                user.OrderInstanceId = instanceId;
                _context.SaveChanges();
            }
        }
    }
}
