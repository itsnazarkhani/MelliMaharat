using System;
using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using MelliMaharat.Models.Enums;

namespace MelliMaharat.Web.Filters
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class AuthorizeByRoleAttribute : Attribute, IAuthorizationFilter
    {
        private readonly UserRoles[] _allowedRoles;

        public AuthorizeByRoleAttribute(params UserRoles[] roles)
        {
            _allowedRoles = roles;
        }

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var user = context.HttpContext.User;

            // Check if user is authenticated
            if (!user.Identity?.IsAuthenticated ?? true)
            {
                context.Result = new RedirectToActionResult("Login", "Authentication", null);
                return;
            }

            // Check if user has required role
            if (_allowedRoles.Length > 0)
            {
                var userRole = user.FindFirst("Role")?.Value;

                if (string.IsNullOrEmpty(userRole) ||
                    !Array.Exists(_allowedRoles, r => r.ToString() == userRole))
                {
                    context.Result = new ForbidResult();
                }
            }
        }
    }
}
