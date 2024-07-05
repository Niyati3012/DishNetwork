using DishNetwork.Entity.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static DishNetwork.Repository.Repository.LoginRepository;

namespace DishNetwork.Repository.Repository.Interfaces
{
    public interface ILoginRepository 
    {
        UserInfo UserExist(UserInfo model);
        public UserInfo UserDetails(string Email);

		List<MenuItem> SetMenu(int? roleid);
        List<MenuItem> SetMenuForUser(int? userId);
    }
}
