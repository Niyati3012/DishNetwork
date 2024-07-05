using DishNetwork.Repository.Repository.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

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

			if (!flage)
			{

                    context.Result = new RedirectToRouteResult(new RouteValueDictionary(new { Controller = "Login", action = "AccessDenied" }));
			}

		}

	}
}
