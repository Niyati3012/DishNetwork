using DishNetwork.Repository.Repository.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.IO;

namespace DishNetwork.Controllers
{
    public class DevicesDashboardController : Controller
    {
        private readonly IDevicesDashboardRepository _devicesDashboardRepository;
        private readonly HttpClient _client;
        public DevicesDashboardController(IDevicesDashboardRepository devicesDashboardRepository)
        {
            _devicesDashboardRepository = devicesDashboardRepository;
            var handler = new HttpClientHandler();
            handler.ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => { return true; };
            _client = new HttpClient(handler);
            //_client.BaseAddress = new Uri("https://71.175.62.13:8051/");
        }
        public IActionResult Index()
        {
            GetAllFilesAsync();
            return View();
        }

        public async Task GetAllFilesAsync()
        {
            var ips = _devicesDashboardRepository.GetAllIPs();

            foreach (var ip in ips)
            {
                _client.BaseAddress = new Uri("https://" + ip.Ipaddress + ":" + ip.Port + "/");
                HttpResponseMessage response = await _client.GetAsync("web/status/dashboard");
                var datetime = DateTime.Now;
                var date = datetime.Date;
                //var time = datetime.ToLocalTime();
                if (response.IsSuccessStatusCode)
                {
                    string data = response.Content.ReadAsStringAsync().Result;

                    // Combine the current directory with the relative directory path
                    string directoryPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Upload", ip.Ipaddress, DateTime.Now.ToString("yyyy/MM/dd"));
                    string fileName = "first_"+ DateTime.Now.ToString("HHmm") + ".txt"; // Define your file name here
                    string filePath = Path.Combine(directoryPath, fileName);

                    try
                    {
                        // Ensure the directory exists
                        if (!Directory.Exists(directoryPath))
                        {
                            Directory.CreateDirectory(directoryPath);
                        }
                    }
                    catch (Exception ex)
                    {
                        throw new Exception(ex.Message);
                    }

                    // Write the data to the file
                    System.IO.File.WriteAllText(filePath, data);

                }
            }
        }

    }
}
