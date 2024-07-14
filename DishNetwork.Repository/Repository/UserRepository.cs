using DishNetwork.Entity.DataContext;
using DishNetwork.Entity.Models;
using DishNetwork.Entity.ViewModels;
using DishNetwork.Repository.Repository.Interfaces;

namespace DishNetwork.Repository.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _context;

        public UserRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public List<UserDetails> GetAllUsers()
        {
            List<UserDetails> users = new List<UserDetails>();
            users = (from user in _context.Users
                     where !user.DeletedAt.HasValue
                     select new UserDetails
                     {
                         UserId = user.UserId,
                         UserName = user.UserName,
                         Email = user.Email,
                         ContactNumber = user.ContactNumber,
                         Menus = string.Join(",",
                                     _context.UserWiseMenus
                                             .Where(x => x.UserId == user.UserId)
                                             .Select(x => x.MenuId)
                                             .ToList()),
                         MenuNames = (from x in _context.UserWiseMenus
                                      join e in _context.Menus on x.MenuId equals e.MenuId
                                      where x.UserId == user.UserId
                                      select e.MenuName).ToList()
                                     ,
                     }).ToList();


            return users;
        }

        public string UserAddEdit(UserDetails userDetails)
        {
            try
            {
                var cuurantAspNetUserId = CV.AspNetUserID();
                if (userDetails.UserId != default)
                {
                    //edit reseller
                    User user = _context.Users.First(e => e.UserId == userDetails.UserId);
                    user.UserName = userDetails.UserName;
                    user.ContactNumber = userDetails.ContactNumber;
                    user.ModifiedDate = DateTime.Now;
                    _context.Users.Update(user);
                    _context.SaveChanges();
                    if (!string.IsNullOrWhiteSpace(userDetails.Menus))
                    {
                        var olduserWiseMenus = _context.UserWiseMenus.Where(x => x.UserId == userDetails.UserId).ToList();

                        // Remove each fetched entity
                        _context.UserWiseMenus.RemoveRange(olduserWiseMenus);

                        // Save the changes to the database
                        _context.SaveChanges();

                        List<int> availableMenus = userDetails.Menus.Split(',').Select(int.Parse).ToList();
                        List<UserWiseMenu> userWiseMenus = new List<UserWiseMenu>();

                        foreach (var item in availableMenus)
                        {

                            UserWiseMenu userWiseMenu = new UserWiseMenu();
                            userWiseMenu.UserId = user.UserId;
                            userWiseMenu.MenuId = item;
                            userWiseMenu.CreatedDate = DateTime.Now;
                            userWiseMenu.CreatedBy = cuurantAspNetUserId;
                            _context.UserWiseMenus.Add(userWiseMenu);
                            _context.SaveChanges();
                        }
                    }
                    return Constant.UserEditSuccessfull;
                }
                else
                {
                    //add User
                    if (!_context.AspNetUsers.Any(e => e.EmailId == userDetails.Email))
                    {

                        AspNetUser aspuser = new AspNetUser
                        {
                            AspNetUserId = Guid.NewGuid().ToString(),
                            EmailId = userDetails.Email,
                            PassWord = "123456",
                            CreatedBy = cuurantAspNetUserId,
                            CreatedDate = DateTime.Now
                        };
                        _context.AspNetUsers.Add(aspuser);
                        _context.SaveChanges();

                        AspNetUserRole aspNetUserRole = new AspNetUserRole
                        {
                            AspNetUserId = aspuser.AspNetUserId,
                            AspNetRoleId = 3
                        };
                        _context.AspNetUserRoles.Add(aspNetUserRole);
                        _context.SaveChanges();

                        var UserID = _context.Resellers.Where(x => x.AspNetUserId == CV.AspNetUserID()).FirstOrDefault().ResellerId;
                        User user = new User
                        {
                            Email = userDetails.Email,
                            UserName = userDetails.UserName,
                            AspNetUserId = aspuser.AspNetUserId,
                            ReSellerId = UserID,
                            ContactNumber = userDetails.ContactNumber,
                            CreatedBy = cuurantAspNetUserId,
                            CreatedDate = DateTime.Now
                        };
                        _context.Users.Add(user);
                        _context.SaveChanges();

                        if (!string.IsNullOrWhiteSpace(userDetails.Menus))
                        {
                            List<int> availableMenus = userDetails.Menus.Split(',').Select(int.Parse).ToList();
                            foreach (var item in availableMenus)
                            {
                                UserWiseMenu userWiseMenu = new UserWiseMenu();
                                userWiseMenu.UserId = user.UserId;
                                userWiseMenu.MenuId = item;
                                userWiseMenu.CreatedDate = DateTime.Now;
                                userWiseMenu.CreatedBy = cuurantAspNetUserId;
                                _context.UserWiseMenus.Add(userWiseMenu);
                                _context.SaveChanges();
                            }
                        }
                        return Constant.UserAdded;
                    }
                    else
                    {
                        return Constant.UserNotAddedEmailExist;
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public User GetUserByAspNetId(string AspNetId)
        {
            return _context.Users.Where(x => x.AspNetUserId == AspNetId).FirstOrDefault();
        }

        public List<Menu> GetAllMenus()
        {
            return _context.Menus.OrderBy(e => e.Sequence).ToList();
        }

        public bool DeleteUser(int userId)
        {
            User user = _context.Users.First(e => e.UserId == userId);
            if (user != null)
            {
                user.DeletedAt = DateTime.Now;
                user.ModifiedDate = DateTime.Now;
                _context.Users.Update(user);
                _context.SaveChanges();
                return true;
            }
            else
            {
                return false;
            }
        }

    }
}
