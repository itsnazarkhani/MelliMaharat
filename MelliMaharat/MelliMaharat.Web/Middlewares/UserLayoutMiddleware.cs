using MelliMaharat.Dal.DbContexts;
using MelliMaharat.Models.Enums;
using MelliMaharat.Web.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace MelliMaharat.Middlewares
{
    public class UserLayoutMiddleware
    {
        private readonly RequestDelegate _next;

        public UserLayoutMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context, ApplicationDbContext db)
        {
            var vm = new LayoutUserViewModel
            {
                FullName = "فلانی",
                ProfileImagePath = "/images/default-avatar.jpg",
                Role = UserRoles.None
            };

            // Check if user logged in
            if (context.User.Identity?.IsAuthenticated == true)
            {
                var username = context.User.Identity.Name;

                var user = await db.Users
                    .Include(x => x.PersonInformation)
                    .FirstOrDefaultAsync(x => x.Username == username);

                if (user != null)
                {
                    // Name
                    vm.FullName = $"{user.PersonInformation.FirstName} {user.PersonInformation.LastName}";

                    // Avatar
                    vm.ProfileImagePath = user.AvatarId == Guid.Empty
                        ? "/images/default-avatar.jpg"
                        : $"/avatars/{user.AvatarId}.jpg";

                    vm.Role = user.Role;

                    // Add role-based navbar items
                    switch (user.Role)
                    {
                        case UserRoles.Student:
                            vm.NavItems.Add(new NavItem { Title = "دروس ترم", Controller = "Student", Action = "TermLessons" });
                            vm.NavItems.Add(new NavItem { Title = "انتخاب واحد", Controller = "Student", Action = "SelectUnits" });
                            vm.NavItems.Add(new NavItem { Title = "نمرات", Controller = "Student", Action = "Scores" });
                            vm.NavItems.Add(new NavItem { Title = "حضور و غیاب", Controller = "Student", Action = "Attendance" });
                            break;

                        case UserRoles.Master:
                            vm.NavItems.Add(new NavItem { Title = "دروس ارائه", Controller = "Master", Action = "PresentedLessons" });
                            break;

                        case UserRoles.Admin:
                            vm.NavItems.Add(new NavItem { Title = "دروس", Controller = "Admin", Action = "Lessons" });
                            vm.NavItems.Add(new NavItem { Title = "اساتید", Controller = "Admin", Action = "Masters" });
                            vm.NavItems.Add(new NavItem { Title = "دانشجویان", Controller = "Admin", Action = "Students" });
                            vm.NavItems.Add(new NavItem { Title = "دروس ارائه شده", Controller = "Admin", Action = "Presentations" });
                            vm.NavItems.Add(new NavItem { Title = "ثبت رویداد", Controller = "Admin", Action = "CreateEvent" });
                            break;
                    }
                }
            }

            context.Items["LayoutUser"] = vm;

            await _next(context);
        }
    }

    public static class UserLayoutMiddlewareExtensions
    {
        public static IApplicationBuilder UseUserLayoutMiddleware(this IApplicationBuilder app)
        {
            return app.UseMiddleware<UserLayoutMiddleware>();
        }
    }
}
