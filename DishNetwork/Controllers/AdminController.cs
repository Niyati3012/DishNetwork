using DishNetwork.Entity.Models;
using DishNetwork.Entity.ViewModels;
using DishNetwork.Repository.Repository.Interfaces;
using Microsoft.AspNetCore.Mvc;


namespace DishNetwork.Controllers
{
    [AuthManager("Admin")]
    public class AdminController : Controller
    {
        private readonly IAdminRepository _adminRepository;

        public AdminController(IAdminRepository adminRepository)
        {
            _adminRepository = adminRepository;
        }

        public IActionResult Index()
        {
            AdminDetails data = new AdminDetails();
            data.adminlist = _adminRepository.GetAllAdmin();

            return View(data);
        }
        public IActionResult AdminAddEdit(AdminDetails adminDetails)
        {
            _adminRepository.AdminAddEdit(adminDetails);
            AdminDetails data = new AdminDetails();
            data.adminlist = _adminRepository.GetAllAdmin();
            return RedirectToAction("Index",data);

        }
        public IActionResult AdminDelete(int AdminId)
        {
            _adminRepository.DeleteAdmin(AdminId);
            return RedirectToAction("Index");
        }
    }
}
