using DishNetwork.Entity.DataContext;
using DishNetwork.Entity.Models;
using DishNetwork.Entity.ViewModels;
using DishNetwork.Repository.Repository.Interfaces;


namespace DishNetwork.Repository.Repository
{
    public class AdminRepository : IAdminRepository
    {
        private readonly ApplicationDbContext _context;

        public AdminRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public List<Admin> GetAllAdmin()
        {
            List<Admin> result = (from admin in _context.Admins
                                  where !admin.DeletedAt.HasValue
                                  select admin).ToList();

            return result;
        }

        public string AdminAddEdit(AdminDetails adminDetails)
        {
            try
            {
                 string? loginUser = CV.AspNetUserID();
                if (adminDetails.AdminId != default)
                {
                    //edit admin
                    Admin admin = _context.Admins.First(e => e.AdminId == adminDetails.AdminId);
                    admin.Name = adminDetails.Name;
                    admin.ModifiedDate = DateTime.Now;
                    admin.ModifiedBy = loginUser;

                    _context.Admins.Update(admin);
                    _context.SaveChanges();
                    return Constant.AdminEditSuccessful;
                }
                else
                {
                    //add admin
                    if (!_context.AspNetUsers.Any(e => e.EmailId == adminDetails.EmailId))
                    {
                        AspNetUser user = new AspNetUser
                        {
                            AspNetUserId = Guid.NewGuid().ToString(),
                            EmailId = adminDetails.EmailId,
                            PassWord = "123456",
                            CreatedBy = loginUser,
                            CreatedDate = DateTime.Now
                        };
                        _context.AspNetUsers.Add(user);
                        _context.SaveChanges();

                        Admin admin = new Admin
                        {
                            EmailId = adminDetails.EmailId,
                            Name = adminDetails.Name,
                            AspNetUserId = user.AspNetUserId,
                            CreatedBy = loginUser,
                            CreatedDate = DateTime.Now
                        };
                        _context.Admins.Add(admin);
                        _context.SaveChanges();

                        AspNetUserRole aspNetUserRole = new AspNetUserRole
                        {
                            AspNetUserId = user.AspNetUserId,
                            AspNetRoleId = 1
                        };
                        _context.AspNetUserRoles.Add(aspNetUserRole);
                        _context.SaveChanges();

                        return Constant.AdminAdded;
                    }
                    else
                    {
                        return Constant.AdminNotAddedEmailExist;
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool DeleteAdmin(int adminId)
        {
            Admin admin = _context.Admins.First(e => e.AdminId == adminId);
            admin.DeletedAt = DateTime.Now;
            _context.Admins.Update(admin);
            _context.SaveChanges();
            return true;
        }

    }
}
