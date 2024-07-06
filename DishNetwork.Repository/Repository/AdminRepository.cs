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

                if (adminDetails.AdminId != default)
                {
                    //edit admin
                    Admin admin = _context.Admins.First(e => e.AdminId == adminDetails.AdminId);
                    admin.Name = adminDetails.Name;
                    admin.ModifiedDate = DateTime.Now;
                    admin.ModifiedBy = "asdf";

                    _context.Admins.Update(admin);
                    _context.SaveChanges();
                    return Constant.AdminEditSuccessful;
                }
                else
                {
                    //add admin
                    //AspNetUser aspnetuser = _context.AspNetUsers.Any(e => e.EmailId == adminDetails.EmailId) ? _context.AspNetUsers.First(e => e.EmailId == adminDetails.EmailId) : new AspNetUser();
                    if (!_context.AspNetUsers.Any(e => e.EmailId == adminDetails.EmailId))
                    {
                        AspNetUser user = new AspNetUser
                        {
                            AspNetUserId = Guid.NewGuid().ToString(),
                            EmailId = adminDetails.EmailId,
                            PassWord = "123456",
                            CreatedBy = "asdf",
                            CreatedDate = DateTime.Now
                        };
                        _context.AspNetUsers.Add(user);
                        _context.SaveChanges();

                        Admin admin = new Admin();
                        admin.EmailId = adminDetails.EmailId;
                        admin.Name = adminDetails.Name;
                        admin.AspNetUserId = user.AspNetUserId;
                        admin.CreatedBy = user.AspNetUserId;
                        admin.CreatedDate = DateTime.Now;
                        _context.Admins.Add(admin);
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
        public bool FileLog(string FileName, string ip)
        {
            try
            {
                Device id = _context.Devices.First(e => e.Ipaddress == ip);
                if (id != null)
                {
                    FileLog fileLog = new FileLog();
                    fileLog.DeviceId = id.DeviceId;
                    fileLog.FirstFile = FileName;
                    fileLog.CreatedBy = "-1";
                    fileLog.CreatedDate = DateTime.Now;
                    _context.FileLogs.Add(fileLog);
                    _context.SaveChanges();
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

    }
}
