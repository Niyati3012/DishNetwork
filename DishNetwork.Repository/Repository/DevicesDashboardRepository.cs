using DishNetwork.Entity.DataContext;
using DishNetwork.Entity.Models;
using DishNetwork.Entity.ViewModels;
using DishNetwork.Repository.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;
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

        //public bool FileLog(string FileName, string ip)
        //{
        //    try
        //    {
        //        var id = _context.Resellers.First(e => e.ResellerId == 2);
        //        if (id != null)
        //        {
        //            FileLog fileLog = new FileLog();
        //            //fileLog.DeviceId = id.DeviceId;
        //            fileLog.FirstFile = FileName;
        //            fileLog.CreatedBy = "-1";
        //            fileLog.CreatedDate = DateTime.Now;
        //            _context.FileLogs.Add(fileLog);
        //            _context.SaveChanges();
        //            return true;
        //        }
        //        return false;
        //    }
        //    catch (Exception ex)
        //    {
        //        return false;
        //    }
        //}
        public async Task<bool> FileLogDb(string filePath, string ip)
        {
            try
            {
                Device device = await _context.Devices.FirstOrDefaultAsync(e => e.Ipaddress == ip);
                FileLog filelog = new FileLog();
                filelog.DeviceId = device.DeviceId;
                filelog.FirstFile = filePath;
                filelog.CreatedDate = DateTime.Now;
                filelog.CreatedBy = "-1";
                await _context.FileLogs.AddAsync(filelog);
                await _context.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        public async Task<List<DevicesDetails>> GetAllIPs()
        {
            try
            {
                List<DevicesDetails> ips = new List<DevicesDetails>();

                ips = await _context.Devices.Select(e => new DevicesDetails()
                {
                    DeviceId = e.DeviceId,
                    Ipaddress = e.Ipaddress,
                    Port = e.Port,

                }).ToListAsync();

                return ips;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
