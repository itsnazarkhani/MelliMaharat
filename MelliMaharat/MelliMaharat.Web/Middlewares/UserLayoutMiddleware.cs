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
                FullName = "فلانی",
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
                                    new LayoutNavItem { Title="دروس ترم", Controller="Student", Action="TermLessons" },
                                    new LayoutNavItem { Title="انتخاب واحد", Controller="Student", Action="SelectUnits" },
                                    new LayoutNavItem { Title="نمرات", Controller="Student", Action="Grades" },
                                    new LayoutNavItem { Title="حضور و غیاب", Controller="Student", Action="Attendance" },
                                });
                                break;

                            case UserRoles.Master:
                                model.NavItems.Add(new LayoutNavItem
                                {
                                    Title = "دروس ارائه",
                                    Controller = "Master",
                                    Action = "PresentedLessons"
                                });
                                break;

                            case UserRoles.Admin:
                                model.NavItems.AddRange(new[]
                                {
                                    new LayoutNavItem { Title="دروس", Controller="admin", Action="Lessons" },
                                    new LayoutNavItem { Title="اساتید", Controller="admin", Action="Masters" },
                                    new LayoutNavItem { Title="دانشجویان", Controller="admin", Action="Students" },
                                    new LayoutNavItem { Title="دروس ارائه شده", Controller="admin", Action="Presentations" },
                                    new LayoutNavItem { Title="ثبت رویداد", Controller="admin", Action="CreateEvent" }
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
