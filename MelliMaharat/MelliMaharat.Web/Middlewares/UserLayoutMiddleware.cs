using System.Security.Claims;
using MelliMaharat.Web.ViewModels;
using Microsoft.AspNetCore.Http;
using MelliMaharat.Dal.UnitOfWork;
using Microsoft.EntityFrameworkCore;
using MelliMaharat.Models.Enums;

namespace MelliMaharat.Web.Middlewares
{
    public class LayoutUserMiddleware
    {
        private readonly RequestDelegate _next;

        public LayoutUserMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context, IUnitOfWork unitOfWork)
        {
            var model = new LayoutUserViewModel
            {
                FullName = "کاربر",
                ProfileImagePath = "/images/default-avatar.jpg",
                Role = UserRoles.None
            };

            var principal = context.User;

            if (principal.Identity?.IsAuthenticated == true)
            {
                var userIdStr = principal.FindFirst(ClaimTypes.NameIdentifier)?.Value;

                if (Guid.TryParse(userIdStr, out var userId))
                {
                    var user = await unitOfWork.Users
                        .GetAll()
                        .Include(u => u.PersonInformation)
                        .FirstOrDefaultAsync(u => u.Id == userId);

                    if (user != null)
                    {
                        model.Role = user.Role;
                        model.FullName = $"{user.PersonInformation.FirstName} {user.PersonInformation.LastName}";
                        model.ProfileImagePath = user.AvatarId == Guid.Empty
                            ? "/images/default-avatar.jpg"
                            : $"/images/uploads/avatars/{user.AvatarId}.jpg";

                        switch (user.Role)
                        {
                            case UserRoles.Student:
                                model.NavItems.AddRange(new[]
                                {
                                    new LayoutNavItem { Title="واحدهای انتخاب‌شده", Controller="Student", Action="Selections" },
                                    new LayoutNavItem { Title="انتخاب واحد", Controller="Student", Action="RegisterSelection" },
                                    new LayoutNavItem { Title="حضور و غیاب", Controller="Student", Action="AttendanceHistory" },
                                    new LayoutNavItem { Title="نمرات و معدل", Controller="Student", Action="Grades" }
                                });
                                break;

                            case UserRoles.Master:
                                model.NavItems.Add(new LayoutNavItem
                                {
                                    Title = "داشبورد",
                                    Controller = "Master",
                                    Action = "Dashboard"
                                });
                                break;

                            case UserRoles.Admin:
                                model.NavItems.AddRange(new[]
                                {
                                    new LayoutNavItem { Title="دروس", Controller="Admin", Action="Lessons" },
                                    new LayoutNavItem { Title="اساتید", Controller="Admin", Action="Masters" },
                                    new LayoutNavItem { Title="دانشجویان", Controller="Admin", Action="Students" },
                                    new LayoutNavItem { Title="ارائه‌ها", Controller="Admin", Action="Presentations" },
                                    new LayoutNavItem { Title="ترم‌های تحصیلی", Controller="Admin", Action="TermsList" },
                                    new LayoutNavItem { Title="زمان‌های انتخاب واحد", Controller="Admin", Action="SelectionEvents" }
                                });
                                break;
                        }
                    }
                }
            }

            context.Items["LayoutUser"] = model;
            await _next(context);
        }
    }

    public static class LayoutUserMiddlewareExtensions
    {
        public static IApplicationBuilder UseLayoutUser(this IApplicationBuilder app)
        {
            return app.UseMiddleware<LayoutUserMiddleware>();
        }
    }
}
