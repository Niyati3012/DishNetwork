using DishNetwork.Entity.Models;
using DishNetwork.Entity.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DishNetwork.Repository.Repository.Interfaces
{
    public interface IAdminRepository
    {
        public List<Admin> GetAllAdmin();
        public string AdminAddEdit(AdminDetails adminDetails);
        public bool DeleteAdmin(int adminId);
    }
}
