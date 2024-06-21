using Microsoft.AspNetCore.Mvc;
using Logic_Layer.Interfaces;
using Database_Layer.DatabaseEntities;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Logic_Layer.Services;
using Logic_Layer.ViewModels;
using System.Net.Http;
using Newtonsoft.Json;
using System.Text;


[Authorize]
public class TransactionController : Controller
{
    private readonly ITransactionLogic _transactionLogic;
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly IConfiguration _configuration;

    public TransactionController(ITransactionLogic transactionLogic, IHttpClientFactory httpClientFactory, IConfiguration configuration)
    {
        _transactionLogic = transactionLogic;
        _httpClientFactory = httpClientFactory;
        _configuration = configuration;
    }

    // GET: Transaction/Create
    public async Task<IActionResult> Transaction()
    {
        var userId = GetUserId();
        var transactions = await _transactionLogic.GetTransactionsByUserIdAsync(userId);
        return View("~/Views/Transaction/Transaction.cshtml", transactions);
    }

    [HttpGet]
    public async Task<IActionResult> Search(string searchTerm)
    {
        var products = await _transactionLogic.SearchProductsAsync(searchTerm);
        var model = new SearchViewModel
        {
            SearchTerm = searchTerm,
            Products = products.Select(p => new ProductViewModel
            {
                ProductId = p.ProductId,
                Title = p.Title,
                Description = p.Description,
                ArtForm = p.ArtForm,
                Price = p.Price
            }).ToList()
        };

        return View(model);
    }

    // POST: Transaction/Create
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> AddToOrderSummary(List<int> productIds)
    {
        if (productIds == null || !productIds.Any())
        {
            return RedirectToAction("MyWork", "MyWork");
        }

        try
        {
            var userId = GetUserId();
            var products = await _transactionLogic.GetProductsByIdsAsync(productIds);
            var totalAmount = products.Sum(p => p.Price);
            var quantity = products.Count;

            var orderSummary = new OrderSummaryViewModel
            {
                UserId = userId,
                TotalAmount = totalAmount,
                Quantity = quantity,
                Products = products.Select(p => new ProductViewModel
                {
                    ProductId = p.ProductId,
                    Title = p.Title,
                    Description = p.Description,
                    ArtForm = p.ArtForm,
                    Price = p.Price,
                    Quantity = 1 // Assuming quantity of 1 for simplicity
                }).ToList()
            };

            return View("OrderSummary", orderSummary);
        }
        catch (Exception ex)
        {
            ModelState.AddModelError(string.Empty, "Unable to create order summary: " + ex.Message);
            return RedirectToAction("MyWork", "MyWork");
        }
    }

    // Confirm the order and start the transaction
    [HttpPost]
    public async Task<IActionResult> ConfirmOrder(OrderSummaryViewModel model)
    {
        await _transactionLogic.ConfirmOrderAsync(model);
        return RedirectToAction("OrderConfirmed", new { userId = model.UserId });
    }

    public IActionResult OrderConfirmed(string userId)
    {
        ViewBag.UserId = userId;
        return View();
    }

    private int GetUserId()
    {
        var userId = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

        if (int.TryParse(userId, out int parsedUserId))
        {
            return parsedUserId;
        }
        else
        {
            // Handle the case where the user ID cannot be parsed
            throw new InvalidOperationException("Unable to retrieve the user ID.");
        }
    }

    // GET: Transaction/ViewTransactions
    public async Task<IActionResult> ViewTransactions()
    {
        // Get the current user's ID
        var userId = GetUserId();

        // Get transactions for the current user
        var transactions = await _transactionLogic.GetTransactionsByUserIdAsync(userId);

        return View(transactions);
    }
    
}