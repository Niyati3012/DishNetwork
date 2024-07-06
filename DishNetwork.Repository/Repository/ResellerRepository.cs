using DishNetwork.Entity.DataContext;
using DishNetwork.Entity.Models;
using DishNetwork.Entity.ViewModels;
using DishNetwork.Repository.Repository.Interfaces;


namespace DishNetwork.Repository.Repository
{
    public class ResellerRepository : IResellerRepository
    {
        private readonly ApplicationDbContext _context;

        public ResellerRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public bool UploadLogo(ResellerDetails model, int ResellerId)
        {

            if (model.Logoimage != null)
            {
                string FilePath = "wwwroot\\UploadedLogo\\" + ResellerId;
                string path = Path.Combine(Directory.GetCurrentDirectory(), FilePath);
                if (Directory.Exists(path))
                {
                    Directory.Delete(path, true);
                    Directory.CreateDirectory(path);
                }
                else
                {
                    Directory.CreateDirectory(path);
                }


                if (model.Logoimage != null)
                {
                    string newfilename = $"{"Logo"}.{Path.GetExtension(model.Logoimage.FileName).Trim('.')}";
                    string fileNameWithPath = Path.Combine(path, newfilename);
                    model.Logo = FilePath.Replace("wwwroot\\UploadedLogo\\", "/UploadedLogo/") + "/" + newfilename;
                    using (var stream = new FileStream(fileNameWithPath, FileMode.Create))
                    {
                        model.Logoimage.CopyTo(stream);
                    }
                    Reseller reseller = _context.Resellers.FirstOrDefault(e => e.ResellerId == ResellerId);
                    if (reseller != null)
                    {
                        reseller.Logo = model.Logo;
                        _context.Resellers.Update(reseller);
                        _context.SaveChanges();
                    }
                }
                return true;
            }

            return false;
        }

        public string ResellerAddEdit(ResellerDetails resellerDetails)
        {
            try
            {
                if (resellerDetails.ResellerId != default)
                {
                    //edit reseller
                    Reseller reseller = _context.Resellers.First(e => e.ResellerId == resellerDetails.ResellerId);
                    reseller.Name = resellerDetails.Name;
                    reseller.ContactNumber = resellerDetails.ContactNumber;
                    reseller.Address1 = resellerDetails.Address1;
                    reseller.Address2 = resellerDetails.Address2;
                    reseller.ZipCode = resellerDetails.ZipCode;
                    reseller.City = resellerDetails.City;
                    reseller.State = resellerDetails.State;
                    reseller.ModifiedDate = DateTime.Now;
                    reseller.ModifiedBy = "asdf";
                    _context.Resellers.Update(reseller);
                    _context.SaveChanges();
                    UploadLogo(resellerDetails, reseller.ResellerId);
                    return Constant.ResellerEditSuccessfull;
                }
                else
                {
                    //add reseller
                    if (!_context.AspNetUsers.Any(e => e.EmailId == resellerDetails.EmailId))
                    {
                        AspNetUser user = new AspNetUser
                        {
                            AspNetUserId = Guid.NewGuid().ToString(),
                            EmailId = resellerDetails.EmailId,
                            PassWord = "123456",
                            CreatedBy = "asdf",
                            CreatedDate = DateTime.Now
                        };
                        _context.AspNetUsers.Add(user);
                        _context.SaveChanges();

                        Reseller reseller = new Reseller();
                        reseller.EmailId = resellerDetails.EmailId;
                        reseller.Name = resellerDetails.Name;
                        reseller.AspNetUserId = user.AspNetUserId;
                        reseller.ContactNumber = resellerDetails.ContactNumber;
                        reseller.Address1 = resellerDetails.Address1;
                        reseller.Address2 = resellerDetails.Address2;
                        reseller.ZipCode = resellerDetails.ZipCode;
                        reseller.City = resellerDetails.City;
                        reseller.State = resellerDetails.State;
                        reseller.CreatedBy = user.AspNetUserId;
                        reseller.CreatedDate = DateTime.Now;
                        _context.Resellers.Add(reseller);
                        _context.SaveChanges();

                        UploadLogo(resellerDetails, reseller.ResellerId);

                        return Constant.ResellerAdded;
                    }
                    else
                    {
                        return Constant.ResellerNotAddedEmailExist;
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }

        }

        public ResellerDetails GetResellerDetail(int? resellerId)
        {
            ResellerDetails resellerdetail = (from reseller in _context.Resellers
                                              where reseller.ResellerId == resellerId
                                              select new ResellerDetails
                                              {
                                                  ResellerId = reseller.ResellerId,
                                                  Name = reseller.Name,
                                                  EmailId = reseller.EmailId,
                                                  ContactNumber = reseller.ContactNumber,
                                                  Address1 = reseller.Address1,
                                                  Address2 = reseller.Address2,
                                                  ZipCode = reseller.ZipCode,
                                                  State = reseller.State,
                                                  City = reseller.City,
                                                  Logo = reseller.Logo,
                                                  ModifiedDate = reseller.ModifiedDate!=null ? reseller.ModifiedDate : reseller.CreatedDate,
                                              }).First();

            return resellerdetail;
        }

        public List<Reseller> GetAllReseller()
        {
            List<Reseller> resellers = new List<Reseller>();
            resellers = _context.Resellers.Where(e=>!e.DeletedAt.HasValue).ToList();

            return resellers;
        }

        public String DeleteReseller(int resellerId)
        {
            Reseller reseller = _context.Resellers.First(e=>e.ResellerId==resellerId);
            if (reseller != null)
            {
                reseller.DeletedAt = DateTime.Now;
                reseller.ModifiedDate = DateTime.Now;
                reseller.ModifiedBy = "asdf";
                _context.Resellers.Update(reseller);
                _context.SaveChanges();

                return Constant.ResellerDeleteSuccessful;
            }
            return Constant.ResellerDeletenotSuccessful;
        }



    }
}
