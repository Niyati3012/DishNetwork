using DishNetwork.Entity.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DishNetwork.Repository.Repository.Interfaces
{
    public interface IDevicesDashboardRepository
    {
        Task<List<DevicesDetails>> GetAllIPs();
        //bool FileLog(string FileName, string ip);
        Task<bool> FileLogDb(string filePath, string ip);
    }
}
