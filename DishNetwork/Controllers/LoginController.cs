using DishNetwork.Constants;
using DishNetwork.Entity.Models;
using DishNetwork.Entity.ViewModels;
using DishNetwork.Repository.Repository;
using DishNetwork.Repository.Repository.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;

namespace DishNetwork.Controllers
{
    public class LoginController : Controller
    {
        private readonly ILoginRepository _loginRepository;
        private readonly IJwtService _jwtService;
        private readonly ISendEmailRepository _sendEmailRepository;
        public LoginController(ILoginRepository loginRepository, IJwtService jwtService, ISendEmailRepository sendEmailRepository)
        {
            _loginRepository = loginRepository;
            _jwtService = jwtService;
            _sendEmailRepository = sendEmailRepository;
        }


        public IActionResult Index()
        {

            return View("Login");
        }
        public IActionResult ForgotPassword()
        {
            return View();
        }
        public static int GetRandomNumeric(int length)
        {
            Random random = new Random();
            const string chars = "0123456789";

            var b = 2;
            int rand = 0;
            for (var i = 1; i <= b; i++)
            {
                var str = new string(Enumerable.Repeat(chars, length).Select(s => s[random.Next(s.Length)]).ToArray());
                rand = Convert.ToInt32(str);
                if (rand >= 100000 && rand <= 999999)
                {
                    break;
                }
                b++;
            }

            return rand;
        }
        public IActionResult LoginUser(UserInfo model)
        {
            UserInfo info = _loginRepository.UserExist(model);


            if (info != null)
            {
                var otp = GetRandomNumeric(6);

                HttpContext.Session.SetString("Email", model.Email);
                HttpContext.Session.SetInt32("otp", otp);

                string To = model.Email;
                string Subject = "Login OTP";
                string Body = "Your otp for " + model.Email + " is " + otp;
                //_sendEmailRepository.SendEmail(To, Subject, Body);

                return RedirectToAction("OTP", "Login");
            }
            return View("Login");
        }

        public IActionResult EmailPassword(string email)
        {
            string To = email;
            string Subject = "Reset password email";
            string Body = "Your password is 123456";
            _sendEmailRepository.SendEmail(To, Subject, Body);

            return RedirectToAction("Index");
        }
        public IActionResult OTP()
        {
            return View();
        }

        public IActionResult OtpValidate(int otp)
        {
            if (!(otp == HttpContext.Session.GetInt32("otp")))
            {
                UserInfo user = new UserInfo();
                user = _loginRepository.UserDetails(HttpContext.Session.GetString("Email"));
                var jwttoken = _jwtService.GenerateJWTAuthetication(user);
                Response.Cookies.Append("jwt", jwttoken);
                return Json(JsonResultData.SetJsonModel(Enums.StatusCode.Ok.GetHashCode(), "Login Successfull!"));
            }
            else
            {
                return RedirectToAction("Index");
            }
           
        }
        public IActionResult Logout()
        {
            Response.Cookies.Delete("jwt");
            return RedirectToAction("Index");
        }
    }
}
