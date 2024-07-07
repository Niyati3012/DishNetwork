using DishNetwork.Entity.Models;
using DishNetwork.Entity.ViewModels;
using DishNetwork.Repository.Repository;
using DishNetwork.Repository.Repository.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Security.Claims;
using static DishNetwork.Repository.Repository.LoginRepository;

namespace DishNetwork.Controllers
{
	[AttributeUsage(AttributeTargets.All)]
	public class LoginAuthManager : Attribute, IAuthorizationFilter
	{
		public void OnAuthorization(AuthorizationFilterContext context)
		{
            var request = context.HttpContext.Request;
            var cookie = request.Cookies["jwt"];
            if (string.IsNullOrEmpty(cookie))
            {
                context.Result = new RedirectToActionResult("Login", "Account", null);
                return;
            }

            try
            {
                var handler = new JwtSecurityTokenHandler();
                var token = handler.ReadJwtToken(cookie);
                // Add additional validation as needed, like checking the expiry date, issuer, audience, etc.

                if (token.ValidTo < DateTime.UtcNow)
                {
                    context.Result = new RedirectToActionResult("Login", "Account", null);
                }
            }
            catch (Exception)
            {
                context.Result = new RedirectToActionResult("Login", "Account", null);
            }
            var jwtService = context.HttpContext.RequestServices.GetService<IJwtService>();
			var loginService = context.HttpContext.RequestServices.GetService<ILoginRepository>();
			var userService = context.HttpContext.RequestServices.GetService<IUserRepository>();

			if (jwtService == null)
			{
				context.Result = new RedirectToRouteResult(new RouteValueDictionary(new { Controller = "Login", action = "Index" }));
				return;
			}

			var request = context.HttpContext.Request;
			var token = request.Cookies["jwt"];

			if (token == null || !jwtService.ValidateToken(token, out JwtSecurityToken jwtToken))
			{
				context.Result = new RedirectToRouteResult(new RouteValueDictionary(new { Controller = "Login", action = "Index" }));
				return;
			}
			var roles = jwtToken.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role);

			if (roles == null)
			{
				context.Result = new RedirectToRouteResult(new RouteValueDictionary(new { Controller = "Login", action = "Index" }));
				return;
			}


        }

	}
}
