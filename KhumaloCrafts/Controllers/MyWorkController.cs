using Microsoft.AspNetCore.Mvc;
using Logic_Layer.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace KhumaloCrafts.Controllers
{
    public class MyWorkController : Controller
    {
        private readonly IProductService _productService;

        public MyWorkController(IProductService productService)
        {
            _productService = productService;
        }

        public async Task<IActionResult> MyWork()
        {
            var artForms = new List<string> { "Sculpture", "Wood Carving", "Painting", "Pottery", "Beadwork", "Glass" };
            ViewBag.ArtForms = artForms;

            // Retrieve your products from the database and pass them to the view
            var productsTask = _productService.GetProductsAsync();

            // Await the task to get the actual result
            var products = await productsTask;

            return View("~/Views/MyWork/MyWork.cshtml", products);
        }
    }
}

