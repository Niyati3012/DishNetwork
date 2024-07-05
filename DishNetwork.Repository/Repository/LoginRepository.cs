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
    public class LoginRepository : ILoginRepository
    {
        private readonly ApplicationDbContext _context;

        public LoginRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        
        public UserInfo UserExist(UserInfo model)
        {
            if(_context.AspNetUsers.Any(e => e.EmailId == model.Email && e.PassWord == model.Password))
            {
                model.AspNetUserId = _context.AspNetUsers.First(e=>e.EmailId == model.Email).AspNetUserId;
                model.Role = _context.AspNetUserRoles.First(e => e.AspNetUserId == model.AspNetUserId).AspNetRoleId;
                model.RoleName = _context.AspNetRoles.First(e => e.AspNetRoleId == model.Role).RoleName;
            }

            return model;
        }
		public UserInfo UserDetails(string Email)
		{
			UserInfo userInfo = new UserInfo();
			userInfo.Email = Email;
			userInfo.AspNetUserId = _context.AspNetUsers.First(e => e.EmailId == Email).AspNetUserId;
			userInfo.Role = _context.AspNetUserRoles.First(e => e.AspNetUserId == userInfo.AspNetUserId).AspNetRoleId;
			userInfo.RoleName = _context.AspNetRoles.First(e => e.AspNetRoleId == userInfo.Role).RoleName;


			return userInfo;
		}
		#region For_Dynamic_Menu
		/// <summary>
		/// This Class for Define Or Store all Static Menu
		/// this class created here because This Class can not in Constant With  Static KeyWord !imp
		/// </summary>
		public class MenuItem
		{
			public string DbName { get; set; }
			public string Label { get; set; }
			public string Url { get; set; }
			public string RoleName { get; set; }
			public string ContollerAction { get; set; }
			public List<string> UrlList { get; set; }
		}

		/// <summary>
		/// List Out The All Static Menu
		/// </summary>
		public List<MenuItem> staticmenu = new List<MenuItem>
		{
			new MenuItem
			  {
				  DbName ="Dashboard",
				  Label = "Dashboard",
				  Url = "/Dashboard",
				  ContollerAction ="/Dashboard/Index",
				  UrlList = new List<string> { "/Admin/Dashboard", "/ViewAction", "/SubmitForm","/Admin/DashBoard" }
			  },
            new MenuItem
              {
                  DbName ="User AddEdit",
                  Label = "User",
                  Url = "/User",
                  ContollerAction ="/User/Index",
                  UrlList = new List<string> { "/Admin/DashBoard", "/ViewAction", "/SubmitForm","/Admin/DashBoard" }
              },
            new MenuItem
              {
                  DbName ="Reseller AddEdit",
                  Label = "Reseller",
                  Url = "/Reseller",
                  ContollerAction ="/Reseller/Index",
                  UrlList = new List<string> { "/Admin/DashBoard", "/ViewAction", "/SubmitForm","/Admin/DashBoard" }
              },
            new MenuItem
              {
                  DbName ="Devices AddEdit",
                  Label = "Devices",
                  Url = "/Devices",
                  ContollerAction ="/Devices/Index",
                  UrlList = new List<string> { "/Admin/DashBoard", "/ViewAction", "/SubmitForm","/Admin/DashBoard" }
              },

            new MenuItem
              {
                  DbName ="Devices Dashboard",
                  Label = "Devices Dashboard",
                  Url = "/DevicesDashboard",
                  ContollerAction ="/DevicesDashboard/Index",
                  UrlList = new List<string> { "/Admin/DashBoard", "/ViewAction", "/SubmitForm","/Admin/DashBoard" }
              },
            new MenuItem
              {
                  DbName ="Admin AddEdit",
                  Label = "Admin",
                  Url = "/Admin",
                  ContollerAction ="/Admin/Index",
                  UrlList = new List<string> { "/Admin/DashBoard", "/ViewAction", "/SubmitForm","/Admin/DashBoard" }
              }
        };
		#endregion

		#region SetMenu
		/// <summary>
		/// With Roleid Get Menu With Database = Static Menu 
		/// </summary>
		/// <param name="roleid"></param>
		/// <returns></returns>
		/// 
		public List<MenuItem> SetMenu(int? roleid)
		{
			List<Menu> MenuItems = null;
			List<MenuItem> Staticmenu = new List<MenuItem>();
			
			if (roleid != null)
			{
				//Set By DataBase
				MenuItems = (from rm in _context.RoleMenus
							 join Menus in _context.Menus
							 on rm.MenuId equals Menus.MenuId into MenusGroup
							 from m in MenusGroup.DefaultIfEmpty()
							 where rm.RoleId == roleid
							 orderby m.Sequence
							 select m).ToList();

				//Set By DataBase And Static Menu
				foreach (Menu menu in MenuItems)
				{
					MenuItem m = new MenuItem();
					m = staticmenu.Where(item => item.DbName == menu.MenuName).FirstOrDefault();

					if (m != null)
					{
				
						Staticmenu.Add(m);
					}

				}
			}
			else
			{
				return Staticmenu;
			}

			return Staticmenu;
		}

        public List<MenuItem> SetMenuForUser(int? userId)
        {
            List<Menu> MenuItems = null;
            List<MenuItem> Staticmenu = new List<MenuItem>();

            if (userId != null)
            {
                //Set By DataBase
                MenuItems = (from rm in _context.UserWiseMenus
                             join Menus in _context.Menus
                             on rm.MenuId equals Menus.MenuId into MenusGroup
                             from m in MenusGroup.DefaultIfEmpty()
                             where rm.UserId == userId
                             orderby m.Sequence
                             select m).ToList();

                //Set By DataBase And Static Menu
                foreach (Menu menu in MenuItems)
                {
                    MenuItem m = new MenuItem();
                    m = staticmenu.Where(item => item.DbName == menu.MenuName).FirstOrDefault();

                    if (m != null)
                    {

                        Staticmenu.Add(m);
                    }

                }
            }
            else
            {
                return Staticmenu;
            }

            return Staticmenu;
        }

        #endregion
    }
}
