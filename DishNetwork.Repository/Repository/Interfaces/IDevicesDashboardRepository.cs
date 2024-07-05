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
        public List<DevicesDetails> GetAllIPs();
    }
}
