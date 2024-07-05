using DishNetwork.Entity.DataContext;
using DishNetwork.Entity.ViewModels;
using DishNetwork.Repository.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DishNetwork.Repository.Repository
{
    public class DevicesDashboardRepository : IDevicesDashboardRepository
    {
        private readonly ApplicationDbContext _context;

        public DevicesDashboardRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public List<DevicesDetails> GetAllIPs()
        {
            List<DevicesDetails> ips = new List<DevicesDetails>();

            ips = _context.Devices.Select(e => new DevicesDetails()
            {
                DeviceId = e.DeviceId,
                Ipaddress = e.Ipaddress,
                Port = e.Port,

            }).ToList();

            return ips;
        }

    }
}
