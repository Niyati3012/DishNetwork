using DishNetwork.Entity.DataContext;
using DishNetwork.Entity.Models;
using DishNetwork.Entity.ViewModels;
using DishNetwork.Repository.Repository;
using DishNetwork.Repository.Repository.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.IO;
using System.Text.Json;

namespace DishNetwork.Controllers
{
    [AuthManager("Reseller,User")]
    public class DevicesDashboardController : Controller
    {
        private readonly IDevicesDashboardRepository _devicesDashboardRepository;
        private readonly IAdminRepository _adminRepository;
        private readonly ApplicationDbContext _context;
        private readonly HttpClient _client;
        private readonly IDashboardRepository _dashboardRepository;
        public DevicesDashboardController(IDevicesDashboardRepository devicesDashboardRepository, IAdminRepository adminRepository, ApplicationDbContext context, IDashboardRepository dashboardRepository)
        {
            _devicesDashboardRepository = devicesDashboardRepository;

            _adminRepository = adminRepository;
            _context = context;
            _dashboardRepository = dashboardRepository;
            //_client.BaseAddress = new Uri("https://71.175.62.13:8051/");
        }
        public async Task<IActionResult> Index()
        {
           await GetAllFilesAsync();

			return View();
        }

        [HttpGet("dishnetwork/devices/getbyid", Name = "ReadDeviceById")]
        public async Task<IActionResult> ReadDeviceStatusAsync(int id)
        {
            try
            {
                var file = _dashboardRepository.FileLog(id);
                if (file != null)
                {
                    string jsonString = await System.IO.File.ReadAllTextAsync(file.FirstFile);
                    JsonDocument doc = JsonDocument.Parse(jsonString);
                    JsonElement root = doc.RootElement;

					//
					var deviceStatus = new
					{
						software = root.GetProperty("data").GetProperty("software").GetProperty("version").ToString(),
						services = new { active = root.GetProperty("data").GetProperty("services").GetProperty("active").ToString(), errored = root.GetProperty("data").GetProperty("services").GetProperty("errored").ToString() },
						transponders = new { active = root.GetProperty("data").GetProperty("transponders").GetProperty("active").ToString(), errored = root.GetProperty("data").GetProperty("transponders").GetProperty("errored").ToString() }
					};
					// Access DataPorts
					var dataPorts = root.GetProperty("data").GetProperty("dataPorts").EnumerateArray();
                    var dataPortsList = new List<object>();
                    foreach (var dataPort in dataPorts)
                    {
                        var port = new
                        {
                            Id = dataPort.GetProperty("id").GetInt32(),
                            Role = dataPort.GetProperty("role").GetString(),
                            Ip = dataPort.GetProperty("ip").GetString(),
                            StatusMessage = dataPort.GetProperty("status").GetProperty("message").GetString()
                        };
                        dataPortsList.Add(port);
                    }

                    // Access CaPools
                    var caPools = root.GetProperty("data").GetProperty("caPools").EnumerateArray();
                    var caPoolsList = new List<object>();
                    foreach (var caPool in caPools)
                    {
                        var pool = new
                        {
                            Name = caPool.GetProperty("name").GetString(),
                            Cams = caPool.GetProperty("cams").EnumerateArray().Select(cam => new
                            {
                                SlotId = cam.GetProperty("slotId").GetInt32(),
                                StatusMessage = cam.GetProperty("status").GetProperty("message").GetString()
                            }).ToList()
                        };
                        caPoolsList.Add(pool);
                    }


                    // Accessing Satellite -> rfInputs
                    var rfInputs = root.GetProperty("data").GetProperty("inputs").GetProperty("satellite").GetProperty("rfInputs").EnumerateArray();
                    var rfInputsList = new List<object>();
                    foreach (var rfInput in rfInputs)
                    {
                        var input = new
                        {
                            RfInputId = rfInput.GetProperty("rfInputId").GetInt32(),
                            Status = new
                            {
                                Color = rfInput.GetProperty("status").GetProperty("color").GetString(),
                                Message = rfInput.GetProperty("status").GetProperty("message").GetString()
                            },
                            SignalStrengthDbm = rfInput.GetProperty("signalStrengthDbm").GetInt32(),
                            SatelliteName = rfInput.GetProperty("satelliteName").GetString(),
                            Services = new
                            {
                                Found = rfInput.GetProperty("services").GetProperty("found").GetInt32(),
                                Active = rfInput.GetProperty("services").GetProperty("active").GetInt32(),
                                Errored = rfInput.GetProperty("services").GetProperty("errored").GetInt32()
                            },
                            Scan = new
                            {
                                ProgressPercent = new
                                {
                                    Initialized = rfInput.GetProperty("scan").GetProperty("progressPercent").GetProperty("initialized").GetBoolean()
                                },
                                LastScanned = new
                                {
                                    Initialized = rfInput.GetProperty("scan").GetProperty("lastScanned").GetProperty("initialized").GetBoolean(),
                                    Value = rfInput.GetProperty("scan").GetProperty("lastScanned").TryGetProperty("value", out JsonElement lastScannedValue) ? lastScannedValue.GetString() : null
                                }
                            }
                        };
                        rfInputsList.Add(input);
                    }

                    // Accessing Satellite -> blades
                    var blades = root.GetProperty("data").GetProperty("inputs").GetProperty("satellite").GetProperty("blades").EnumerateArray();
                    var bladesList = new List<object>();
                    foreach (var blade in blades)
                    {
                        var tuners = blade.GetProperty("tuners").EnumerateArray();
                        var tunersList = new List<object>();
                        foreach (var tuner in tuners)
                        {
                            var tunerObj = new
                            {
                                TunerId = tuner.GetProperty("tunerId").GetInt32(),
                                Status = new
                                {
                                    Color = tuner.GetProperty("status").GetProperty("color").GetString(),
                                    Message = tuner.GetProperty("status").GetProperty("message").GetString()
                                }
                            };
                            tunersList.Add(tunerObj);
                        }

                        var bladeObj = new
                        {
                            SlotId = blade.GetProperty("slotId").GetInt32(),
                            Tuners = tunersList
                        };
                        bladesList.Add(bladeObj);
                    }

                    var satellite = new
                    {
                        RfInputs = rfInputsList,
                        Blades = bladesList
                    };


                    // Accessing ATSC Groups
                    var atscGroups = root.GetProperty("data").GetProperty("inputs").GetProperty("atscGroups").EnumerateArray();
                    var atscGroupsList = new List<object>();
                    foreach (var atscGroup in atscGroups)
                    {
                        var atscBlades = atscGroup.GetProperty("blades").EnumerateArray();
                        var atscBladesList = new List<object>();
                        foreach (var blade in atscBlades)
                        {
                            var tuners = blade.GetProperty("tuners").EnumerateArray();
                            var atscTunersList = new List<object>();
                            foreach (var tuner in tuners)
                            {
                                var tunerObj = new
                                {
                                    TunerId = tuner.GetProperty("tunerId").GetInt32(),
                                    Status = new
                                    {
                                        Color = tuner.GetProperty("status").GetProperty("color").GetString(),
                                        Message = tuner.GetProperty("status").GetProperty("message").GetString()
                                    }
                                };
                                atscTunersList.Add(tunerObj);
                            }

                            var bladeObj = new
                            {
                                SlotId = blade.GetProperty("slotId").GetInt32(),
                                Tuners = atscTunersList
                            };
                            atscBladesList.Add(bladeObj);
                        }

                        var group = new
                        {
                            Id = atscGroup.GetProperty("id").GetString(),
                            Name = atscGroup.GetProperty("name").GetString(),
                            Services = new
                            {
                                Found = atscGroup.GetProperty("services").GetProperty("found").GetInt32(),
                                Active = atscGroup.GetProperty("services").GetProperty("active").GetInt32(),
                                Errored = atscGroup.GetProperty("services").GetProperty("errored").GetInt32()
                            },
                            Scan = new
                            {
                                ProgressPercent = new
                                {
                                    Initialized = atscGroup.GetProperty("scan").GetProperty("progressPercent").GetProperty("initialized").GetBoolean()
                                },
                                LastScanned = new
                                {
                                    Initialized = atscGroup.GetProperty("scan").GetProperty("lastScanned").GetProperty("initialized").GetBoolean(),
                                    Value = atscGroup.GetProperty("scan").GetProperty("lastScanned").TryGetProperty("value", out JsonElement lastScannedValue) ? lastScannedValue.GetString() : null
                                }
                            },
                            Blades = atscBladesList
                        };
                        atscGroupsList.Add(group);
                    }
                    var qamBladesList = new List<object>();
                    if (root.TryGetProperty("data", out JsonElement data) &&
                          data.TryGetProperty("outputNetworks", out JsonElement outputNetworks) &&
                          outputNetworks.TryGetProperty("qam", out JsonElement qam))
                    {
                        var qamNetworks = qam.EnumerateArray();
                        

                        foreach (var qamNetwork in qamNetworks)
                        {
                            if (qamNetwork.TryGetProperty("blades", out JsonElement blades1))
                            {
                                foreach (var blade in blades1.EnumerateArray())
                                {
                                    if (blade.TryGetProperty("rfCarriers", out JsonElement rfCarriers))
                                    {
                                        var rfCarriersList = new List<object>();

                                        foreach (var rfCarrier in rfCarriers.EnumerateArray())
                                        {
                                            var carrierObj = new
                                            {
                                                RfCarrierId = rfCarrier.GetProperty("rfCarrierId").GetInt32(),
                                                Status = new
                                                {
                                                    Color = rfCarrier.GetProperty("status").GetProperty("color").GetString(),
                                                    Message = rfCarrier.GetProperty("status").GetProperty("message").GetString()
                                                }
                                            };
                                            rfCarriersList.Add(carrierObj);
                                        }

                                        var bladeObj = new
                                        {
                                            SlotId = blade.GetProperty("slotId").GetInt32(),
                                            RfCarriers = rfCarriersList
                                        };
                                        qamBladesList.Add(bladeObj);
                                    }
                                }
                            }
                        }

                      
                    }


                    var result = new
                    {



						DeviceStatus = deviceStatus,
						DataPorts = dataPortsList,
                        CaPools = caPoolsList,
                        Satellite = satellite,
                        AtscGroups = atscGroupsList,
                        OutputNetworks = new
                        {
                            Qam = qamBladesList
                        }
                    };

                    return Json(result);
                }
                return Json(new { error = "File not found" });
            }
            catch (Exception ex)
            {
                return Json(new { error = ex.Message });
            }
        }

        [HttpGet("dishnetwork/devices/getall")]
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
