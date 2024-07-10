using DishNetwork.Entity.Models;
using DishNetwork.Entity.ViewModels;
using DishNetwork.Repository.Repository;
using DishNetwork.Repository.Repository.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using static DishNetwork.Repository.Repository.LoginRepository;

namespace DishNetwork.Controllers
{
	[AttributeUsage(AttributeTargets.All)]
	public class AuthManager : Attribute, IAuthorizationFilter
	{
		private readonly List<string> _role;
		public AuthManager(string role = "")
		{
			_role = role.Split(',').ToList();
		}
		public void OnAuthorization(AuthorizationFilterContext context)
		{
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


			bool flage = false;

			foreach (var role in _role)
			{
				if (string.IsNullOrWhiteSpace(role) || roles.Value != role)
				{
                    flage = false;
                }
                else
				{
					flage = true;
					break;
				}
			}
			if (Convert.ToInt32(CV.RoleId()) == 3)
			{
                var Path = context.HttpContext.Request.Path;

                var userId = userService.GetUserByAspNetId(CV.AspNetUserID());
                List<MenuItem> Staticmenu = loginService.SetMenuForUser((int)userId.UserId);


                bool isPathAvailable = Staticmenu.Any(item =>
                    item.Url.Equals(Path, StringComparison.OrdinalIgnoreCase)
                    );
                if ((Staticmenu == null || !isPathAvailable))
                {
                    context.Result = new RedirectToRouteResult(new RouteValueDictionary(new { Controller = "Login", action = "AccessDenied" }));

                }
            }
            if ( !flage )
            {
                context.Result = new RedirectToRouteResult(new RouteValueDictionary(new { Controller = "Login", action = "AccessDenied" }));

            } 

        }

	}
}
