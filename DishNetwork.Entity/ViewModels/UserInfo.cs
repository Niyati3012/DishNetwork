using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace DishNetwork.Entity.ViewModels
{
	public class UserInfo
	{
		public string AspNetUserId { get; set; }
		public string Email { get; set; }
		public string Password { get; set; }
		public string RoleName { get; set; }
		public int Role {  get; set; }
		
	}
}
