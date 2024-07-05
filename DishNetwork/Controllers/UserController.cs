using DishNetwork.Entity.Models;
using DishNetwork.Entity.ViewModels;
using DishNetwork.Repository.Repository.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace DishNetwork.Controllers
{
	[AuthManager("User")]
	public class UserController : Controller
    {
        private readonly IUserRepository _userRepository;
        public UserController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        public IActionResult Index()
        {
            ViewBag.Menus = _userRepository.GetAllMenus();
            UserDetails userDetails = new UserDetails();
            List<UserDetails> users = _userRepository.GetAllUsers();
            userDetails.Users = users;
            return View(userDetails);
        }
        public IActionResult UserAddEdit(UserDetails userDetails)
        {
            _userRepository.UserAddEdit(userDetails);
            return RedirectToAction("Index");
        }

        public IActionResult DeleteUser(int id)
        {
            _userRepository.DeleteUser(id);
            return RedirectToAction("Index");
        }
    }
}
