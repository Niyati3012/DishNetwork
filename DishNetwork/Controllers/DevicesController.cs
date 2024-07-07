using DishNetwork.Entity.Models;
using DishNetwork.Entity.ViewModels;
using DishNetwork.Repository.Repository;
using DishNetwork.Repository.Repository.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace DishNetwork.Controllers
{
    [AuthManager("Reseller,User")]
    public class DevicesController : Controller
    {
        private readonly IDevicesRepository _devicesRepository;
        public DevicesController(IDevicesRepository devicesRepository)
        {
            _devicesRepository = devicesRepository;
        }
        
        public IActionResult Index()
        {
            List<Device> data = _devicesRepository.GetAllDevices();
            return View(data);
        }
        public IActionResult DevicesAddEdit(int? id)
        {
            if (id.HasValue)
            {
                return View(_devicesRepository.GetDevicesDetails((int)id));
            }
            else
            {
                return View();

            }

        }
        public IActionResult AddEditDevice(DevicesDetails details)
        {
            _devicesRepository.DevicesAddEdit(details);
            return RedirectToAction("Index");
        }
        public IActionResult DeleteDevices(int id)
        {
            _devicesRepository.DeleteDevices(id);
            return RedirectToAction("Index");
        }

    }
}
