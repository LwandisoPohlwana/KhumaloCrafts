using KhumaloCrafts.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace KhumaloCraft.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult MyWork()
        {
            List<Product> products = GetProducts();
            return View(products);
        }

        private static List<Product> GetProducts()
        {
            return new List<Product>
    {
        new Product(1, "Blue Pink Sky", "Painting", "Mike Melly", "An art work about blue and pink skys at the beach", 130.00m, "../css/painting/1.jpeg"),
        new Product(2, "RED RED", "Painting", "Emmely Sun", "Beautiful red hair lady", 120.00m, "../css/painting/2.jpeg"),
        new Product(3, "Lake in the Forest", "Painting", "Sam Ballo", "Lake in the forest, good place to get away", 105.00m, "../css/painting/8.jpeg"),
        new Product(4, "Golden", "Pottery", "Antony Ball", "Bowel with a hint of gold", 90.00m, "../css/pottery/2.jpg"),
        new Product(5, "Jug on the sky", "Pottery", "Solly Vrea", "Sky blue jug with a grip of nature", 100.00m, "../css/pottery/5.jpg"),
        new Product(6, "White vase", "Pottery", "Sun Yong", "Family on a vase", 110.00m, "../css/pottery/6.jpeg"),
        new Product(7, "Kabin", "Pottery", "Sam Lee", "Kabin as art", 105.00m, "../css/pottery/8.jpeg"),
        new Product(8, "Daiyu", "Sculpture", "Bao Lao", "Beautiful chinese lady", 180.00m, "../css/sculpture/3.jpeg"),
        new Product(9, "Man with a thought", "Sculpture", "Bob Antony", "A simple thought", 140.00m, "../css/sculpture/4.jpeg"),
        new Product(10, "Mazibula", "Sculpture", "Samkele Masango", "An art work for my moms", 175.00m, "../css/sculpture/10.jpeg"),
        new Product(11, "Egle", "Wood Carving", "Willy Wood", "Egles fly high", 160.00m, "../css/wood/1.jpeg"),
        new Product(12, "Faces", "Wood Carving", "Molife Mabaso", "All the faces", 90.00m, "../css/wood/2.jpeg"),
        new Product(13, "Wooden", "Wood Carving", "Yanda Vaiola", "Wall of wood", 100.00m, "../css/wood/3.jpeg"),
        new Product(14, "Waves", "Glass", "Rick Miller", "Waves from shore", 90.00m, "../css/glass/9.jpeg"),
        new Product(15, "Rock of glass", "Glass", "Lilly Base", "ROCK OF GLASS", 75.00m, "../css/glass/8.jpeg"),
        new Product(16, "Bonsai", "Glass", "Broly Oraen", "Bonsai made with glass", 125.00m, "../css/glass/7.jpeg"),
        new Product(17, "Native", "Beadwork", "Que Lord", "Native American", 90.00m, "../css/beadwork/2.jpeg"),
        new Product(18, "Africa", "Beadwork", "Sam Willams", "African rital", 75.00m, "../css/beadwork/3.jpeg"),
        new Product(19, "Nack of beads", "Beadwork", "Amkelani momo", "Beadwork necklace", 75.00m, "../css/beadwork/4.jpeg"),
        // Add more products as needed
    };
        }

        public IActionResult About()
        {
            return View();
        }

        public IActionResult Contuct()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
