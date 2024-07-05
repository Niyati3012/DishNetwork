using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace DishNetwork.Controllers
{
    [AuthManager("Admin,Reseller,User")]
    public class DashboardController : Controller
    {
		private readonly HttpClient _client;

		public DashboardController()
        {
			var handler = new HttpClientHandler();
			handler.ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => { return true; };
			_client = new HttpClient(handler);
			_client.BaseAddress = new Uri("https://71.175.62.13:8051/");
		}
        [HttpGet]
        public async Task<IActionResult> IndexAsync()
        {
            // Read and search the JSON file
            string jsonString = await System.IO.File.ReadAllTextAsync("C:/Users/Niyati/Desktop/Project/DishNetwork/wwwroot/Upload/71.175.62.13/2024-07-01/data.txt");
            using (JsonDocument doc = JsonDocument.Parse(jsonString))
            {
                JsonElement root = doc.RootElement;

                // Specify the key you want to search for
                string keyToSearch = "date";

                if (root.TryGetProperty(keyToSearch, out JsonElement value))
                {
                    Console.WriteLine($"Key '{keyToSearch}' found with value: {value}");
                }
                else
                {
                    Console.WriteLine($"Key '{keyToSearch}' not found.");
                }
            }
            return View();
		}
    }
}
