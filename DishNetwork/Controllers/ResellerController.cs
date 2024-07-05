using DishNetwork.Entity.ViewModels;
using DishNetwork.Repository.Repository.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace DishNetwork.Controllers
{
    public class ResellerController : Controller
    {
        private readonly IResellerRepository _resellerRepository;

        public ResellerController(IResellerRepository resellerRepository)
        {
            _resellerRepository = resellerRepository;
        }
        public IActionResult Index()
        {

            ResellerDetails details = new ResellerDetails();
            details.Resellers = _resellerRepository.GetAllReseller();
            return View(details);
        }
        public IActionResult ResellerAddEdit(int? ResellerId)
        {
            if (ResellerId.HasValue)
            {
                ResellerDetails data = _resellerRepository.GetResellerDetail(ResellerId);
                return View(data);
            }
            return View();

        }

        public IActionResult AddEditReseller(ResellerDetails details)
        {
            _resellerRepository.ResellerAddEdit(details);
            return RedirectToAction("Index");
        }

        public IActionResult ResellerDelete(int? ResellerId)
        {
            if (ResellerId.HasValue)
            {
                _resellerRepository.DeleteReseller((int)ResellerId);
                return RedirectToAction("Index");
            }
            else
            {
                return RedirectToAction("Index");

            }
        }
    }
}
