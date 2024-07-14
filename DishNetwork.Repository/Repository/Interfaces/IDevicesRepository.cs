using DishNetwork.Entity.Models;
using DishNetwork.Entity.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DishNetwork.Repository.Repository.Interfaces
{
    public interface IDevicesRepository
    {
        public List<Device> GetAllDevices();
        public String DevicesAddEdit(DevicesDetails device);
        public DevicesDetails GetDevicesDetails(int DeviceId);
        public string DeleteDevices(int DeviceId);
        
    }
}
