using DishNetwork.Entity.Models;
using DishNetwork.Entity.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DishNetwork.Repository.Repository.Interfaces
{
	public interface IUserRepository
	{
		List<UserDetails> GetAllUsers();
		List<Menu> GetAllMenus();
		string UserAddEdit(UserDetails userDetails);
		bool DeleteUser(int userId);
		User GetUserByAspNetId(string AspNetId);


    }
}
