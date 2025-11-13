using Microsoft.AspNetCore.Mvc;
using PayMopIntegration.Entities;
using PayMopIntegration.Interfaces;
using PayMopIntegration.Models;
using System.Diagnostics;
using System.Text.Json;
using System.Text;

namespace PayMopIntegration.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly HttpClient _httpClient;
        public HomeController(ILogger<HomeController> logger, HttpClient httpClient)
        {
            _logger = logger;
            _httpClient = httpClient;
        }

        public  IActionResult Index()
        {
            return View();
        }

        public  async Task<IActionResult> enroll(int studentId, int courseId)
        {
            var data = new
            {
                StudentId = studentId,
                CourseId = courseId
            };

            var json = new StringContent(
                JsonSerializer.Serialize(data),
                Encoding.UTF8,
                "application/json"
            );

            
            var response = await _httpClient.PostAsync("https://localhost:5001/api/paymentsapi/create", json);
            if (!response.IsSuccessStatusCode)
                return Content("Error creating payment");

            var Resultjson = await response.Content.ReadAsStringAsync();
            var obj = System.Text.Json.JsonSerializer.Deserialize<System.Text.Json.JsonElement>(Resultjson);
            var paymentUrl = obj.GetProperty("PaymentUrl").GetString();

            ViewBag.PaymentUrl = paymentUrl;
            return View("Payment");
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
