using DishNetwork.Entity.ViewModels;
using DishNetwork.Repository.Repository.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace DishNetwork.Controllers
{
    [AuthManager("Admin,Reseller,User")]
    public class DashboardController : Controller
    {
		private readonly HttpClient _client;
        private readonly IDashboardRepository _dashboardRepository;
		public DashboardController(IDashboardRepository dashboardRepository)
        {
			var handler = new HttpClientHandler();
			handler.ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => { return true; };
			_client = new HttpClient(handler);
			_client.BaseAddress = new Uri("https://71.175.62.13:8051/");
            _dashboardRepository = dashboardRepository;
		}
        [HttpGet]
        public async Task<IActionResult> IndexAsync()
        {
            // Read and search the JSON file
            string jsonString = await System.IO.File.ReadAllTextAsync("C:/Users/Niyati/Desktop/Project/DishNetwork/wwwroot/Upload/71.175.62.13/2024-07-01/data.txt");
			JsonDocument doc = JsonDocument.Parse(jsonString);
			JsonElement root = doc.RootElement;

			// Access DataPorts
			JsonElement dataPorts = root.GetProperty("data").GetProperty("dataPorts");
			foreach (JsonElement dataPort in dataPorts.EnumerateArray())
			{
				int id = dataPort.GetProperty("id").GetInt32();
				string role = dataPort.GetProperty("role").GetString();
				string ip = dataPort.GetProperty("ip").GetString();
				string statusMessage = dataPort.GetProperty("status").GetProperty("message").GetString();

				Console.WriteLine($"DataPort ID: {id}, Role: {role}, IP: {ip}, Status: {statusMessage}");
			}

			// Access CaPools
			JsonElement caPools = root.GetProperty("data").GetProperty("caPools");
			foreach (JsonElement caPool in caPools.EnumerateArray())
			{
				string name = caPool.GetProperty("name").GetString();
				Console.WriteLine($"CaPool Name: {name}");
				JsonElement cams = caPool.GetProperty("cams");
				foreach (JsonElement cam in cams.EnumerateArray())
				{
					int slotId = cam.GetProperty("slotId").GetInt32();
					string statusMessage = cam.GetProperty("status").GetProperty("message").GetString();

					Console.WriteLine($"  Cam Slot ID: {slotId}, Status: {statusMessage}");
				}
			}
			return View();
		}

		static void SearchJson(JsonElement element, string searchKey, List<JsonElement> foundElements)
		{
			foreach (JsonProperty property in element.EnumerateObject())
			{
				if (property.Name == searchKey)
				{
					foundElements.Add(property.Value);
				}

				if (property.Value.ValueKind == JsonValueKind.Object)
				{
					SearchJson(property.Value, searchKey, foundElements);
				}
				else if (property.Value.ValueKind == JsonValueKind.Array)
				{
					foreach (JsonElement item in property.Value.EnumerateArray())
					{
						if (item.ValueKind == JsonValueKind.Object)
						{
							SearchJson(item, searchKey, foundElements);
						}
					}
				}
			}
		}
		public IActionResult GetLocations()
        {
            return  Json(_dashboardRepository.DeviceLocationDetails());
        }
    }
}
