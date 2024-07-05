using DishNetwork.Entity.DataContext;
using DishNetwork.Entity.Models;
using DishNetwork.Entity.ViewModels;
using DishNetwork.Repository.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DishNetwork.Repository.Repository
{
    public class DevicesRepository : IDevicesRepository
    {
        private readonly ApplicationDbContext _context;

        public DevicesRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public List<Device> GetAllDevices()
        {
            List<Device> devices = new List<Device>();
            devices = _context.Devices.Where(e => !e.DeletedAt.HasValue).ToList();

            return devices;
        }

        public String DevicesAddEdit(DevicesDetails device)
        {
            Device device1 = device.DeviceId != default ? _context.Devices.First(e => e.DeviceId == device.DeviceId) : new Device();
            device1.ReSellerId = 2;
            device1.Ipaddress = device.Ipaddress;
            device1.Port = device.Port;
            device1.PersonName = device.PersonName;
            device1.PropertyName = device.PropertyName;
            device1.NoOfRooms = device.NoOfRooms;
            device1.Equipment = device.Equipment;
            device1.ExpiryDate = device.ExpiryDate;
            device1.MaintainanceContract = device.MaintainanceContract;
            device1.Address = device.Address;
            device1.State = device.State;
            device1.City = device.City;
            device1.ZipCode = device.ZipCode;
            device1.GoogleMap = device.GoogleMap;
            device1.EmailId = device.EmailId;
            device1.ContactNo = device.ContactNo;
            device1.VpnUserName = device.VpnUserName;
            device1.VpnPassword = device.VpnPassword;

            InsertOrUpdateDevice(device1);
            if (device.DeviceId == default)
            {
                return Constant.DeviceAdded;
            }
            else
            {
                return Constant.DeviceEditSuccessfull;
            }

        }

        public bool InsertOrUpdateDevice(Device device1)
        {
            if (device1 == null) return false;
            else
            {
                if (device1.DeviceId == default)
                {
                    device1.CreatedDate = DateTime.Now;
                    device1.CreatedBy = "asdf";
                    _context.Devices.Add(device1);
                }
                else
                {
                    device1.ModifiedDate = DateTime.Now;
                    _context.Update(device1);
                }
                _context.SaveChanges();
                return true;
            }
        }

        public DevicesDetails GetDevicesDetails(int DeviceId)
        {
            //DevicesDetails details = new DevicesDetails();

            DevicesDetails details = (from device in _context.Devices
                      where device.DeviceId == DeviceId
                      select new DevicesDetails
                      {
                          DeviceId = device.DeviceId,
                          ReSellerId = device.ReSellerId,
                          Ipaddress = device.Ipaddress,
                          Port = device.Port,
                          PersonName = device.PersonName,
                          PropertyName = device.PropertyName,
                          NoOfRooms = device.NoOfRooms,
                          Equipment = device.Equipment,
                          ExpiryDate = device.ExpiryDate,
                          MaintainanceContract = device.MaintainanceContract,
                          Address = device.Address,
                          State = device.State,
                          City = device.City,
                          ZipCode = device.ZipCode,
                          GoogleMap = device.GoogleMap,
                          EmailId = device.EmailId,
                          ContactNo = device.ContactNo,
                          VpnUserName = device.VpnUserName,
                          VpnPassword = device.VpnPassword,
                      }).First();
            return details;
        }

        public string DeleteDevices(int DeviceId)
        {
            Device device = _context.Devices.First(x => x.DeviceId == DeviceId);
            if (device != null)
            {
                device.DeletedAt = DateTime.Now;
                InsertOrUpdateDevice(device);
                return Constant.UserDeleteSuccessful;
            }
            return Constant.DeviceDeleteUnSuccessfull;
        }
    }
}
