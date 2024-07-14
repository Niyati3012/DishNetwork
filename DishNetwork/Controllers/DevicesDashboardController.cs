using DishNetwork.Entity.DataContext;
using DishNetwork.Entity.Models;
using DishNetwork.Entity.ViewModels;
using DishNetwork.Repository.Repository.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.IO;

namespace DishNetwork.Controllers
{
    [AuthManager("Reseller,User")]
    public class DevicesDashboardController : Controller
    {
        private readonly IDevicesDashboardRepository _devicesDashboardRepository;
        private readonly IAdminRepository _adminRepository;
        private readonly ApplicationDbContext _context;
        private readonly HttpClient _client;
        public DevicesDashboardController(IDevicesDashboardRepository devicesDashboardRepository, IAdminRepository adminRepository, ApplicationDbContext context)
        {
            _devicesDashboardRepository = devicesDashboardRepository;

            _adminRepository = adminRepository;
            _context = context;
            //_client.BaseAddress = new Uri("https://71.175.62.13:8051/");
        }
        public async Task<IActionResult> Index()
        {
            await GetAllFilesAsync();

            return View();
        }

        public async Task GetAllFilesAsync()
        {

            var ips = await _devicesDashboardRepository.GetAllIPs();

            foreach (var ip in ips)
            {
                using (var handler = new HttpClientHandler())
                {
                    handler.ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => true;

                    using (var client = new HttpClient(handler))
                    {
                        client.BaseAddress = new Uri($"https://{ip.Ipaddress}:{ip.Port}/");
                        HttpResponseMessage response = new HttpResponseMessage();
                        try
                        {
                            response = await client.GetAsync("web/status/dashboard");

                        }
                        catch (Exception ex)
                        {

                            // Log the exception or handle it appropriately
                            Console.WriteLine($"An error occurred: {ex.Message}");
                        }


                        if (response.IsSuccessStatusCode)
                        {
                            string data = await response.Content.ReadAsStringAsync();

                            string directoryPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Upload", ip.Ipaddress, DateTime.Now.ToString("yyyy/MM/dd"));
                            string fileName = $"first_{DateTime.Now.ToString("HHmm")}.txt";
                            string filePath = Path.Combine(directoryPath, fileName);

                            if (!Directory.Exists(directoryPath))
                            {
                                Directory.CreateDirectory(directoryPath);
                            }

                            System.IO.File.WriteAllText(filePath, data);
                            await _devicesDashboardRepository.FileLogDb(filePath, ip.Ipaddress);
                        }
                        else
                        {
                            // Customize your data here
                            bool customData = false;
                            string data = $"{{ \"data\": {customData.ToString().ToLower()} }}";
                            string directoryPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Upload", ip.Ipaddress, DateTime.Now.ToString("yyyy/MM/dd"));
                            string fileName = $"first_{DateTime.Now.ToString("HHmm")}.txt";
                            string filePath = Path.Combine(directoryPath, fileName);

                            if (!Directory.Exists(directoryPath))
                            {
                                Directory.CreateDirectory(directoryPath);
                            }

                            System.IO.File.WriteAllText(filePath, data);
                            await _devicesDashboardRepository.FileLogDb(filePath, ip.Ipaddress);

                        }
                    }
                }
            }

        }

    }
}
