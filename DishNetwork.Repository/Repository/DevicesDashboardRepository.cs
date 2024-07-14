using DishNetwork.Entity.DataContext;
using DishNetwork.Entity.Models;
using DishNetwork.Entity.ViewModels;
using DishNetwork.Repository.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DishNetwork.Repository.Repository
{
    public class DevicesDashboardRepository : IDevicesDashboardRepository
    {
        private readonly ApplicationDbContext _context;

        public DevicesDashboardRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<bool> FileLogDb(string filePath, string ip)
        {
            try
            {
                Device device = await _context.Devices.FirstAsync(e => e.Ipaddress == ip);
                FileLog filelog = new FileLog
                {
                    DeviceId = device.DeviceId,
                    FirstFile = filePath,
                    CreatedDate = DateTime.Now,
                    CreatedBy = "-1"
                };
                await _context.FileLogs.AddAsync(filelog);
                await _context.SaveChangesAsync();

                return true;
            }
            catch (Exception )
            {
                return false;
            }
        }
        public async Task<List<DevicesDetails>> GetAllIPs()
        {
            try
            {
                List<DevicesDetails> ips = new List<DevicesDetails>();

                ips = await _context.Devices.Where(e=>!e.DeletedAt.HasValue).Select(e => new DevicesDetails()
                {
                    DeviceId = e.DeviceId,
                    Ipaddress = e.Ipaddress,
                    Port = e.Port,

                }).ToListAsync();

                return ips;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
