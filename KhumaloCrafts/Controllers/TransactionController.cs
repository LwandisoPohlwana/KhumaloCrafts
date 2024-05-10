using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Logic_Layer.Interfaces;
using Database_Layer.DatabaseEntities;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;


[Authorize]
public class TransactionController : Controller
{
    private readonly ITransactionLogicService _transactionLogicService;

    public TransactionController(ITransactionLogicService transactionLogicService)
    {
        _transactionLogicService = transactionLogicService;
    }

    // GET: Transaction/Create
    public async Task<IActionResult> Transaction()
    {
        // Get the current user's ID
        var userId = GetUserId();

        // Get transactions for the current user
        var transactions = await _transactionLogicService.GetTransactionsByUserIdAsync(userId);

        return View("~/Views/Transaction/Transaction.cshtml", transactions);
    }

    // POST: Transaction/Create
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(List<int> productIds)
    {
        if (productIds == null || !productIds.Any())
        {
            // No products selected, return to the MyWork view
            return RedirectToAction("MyWork", "MyWork");
        }

        try
        {
            // Get the current user ID
            var userId = GetUserId();

            // Calculate total amount and quantity based on products
            decimal totalAmount = 0;
            int quantity = 0;
            var products = await _transactionLogicService.GetProductsByIdsAsync(productIds);
            foreach (var product in products)
            {
                totalAmount += product.Price;
                quantity++;
            }

            // Create the transaction
            await _transactionLogicService.CreateTransactionAsync(userId, productIds, quantity, totalAmount, DateTime.Now);

            return RedirectToAction("Transaction", "Transaction");
        }
        catch (Exception ex)
        {
            ModelState.AddModelError(string.Empty, "Unable to create transaction: " + ex.Message);
            // Handle the error - for example, display an error message or return to the MyWork view
            return RedirectToAction("MyWork", "MyWork");
        }
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
            // For example, return a default user ID or throw an exception
            throw new InvalidOperationException("Unable to retrieve the user ID.");
        }
    }
    // GET: Transaction/ViewTransactions
    public async Task<IActionResult> ViewTransactions()
    {
        // Get the current user's ID
        var userId = GetUserId();

        // Get transactions for the current user
        var transactions = await _transactionLogicService.GetTransactionsByUserIdAsync(userId);

        return View(transactions);
    }

    // Helper method to get the current user ID
    
}