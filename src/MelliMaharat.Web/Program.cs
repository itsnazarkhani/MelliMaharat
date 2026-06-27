using FluentValidation;
using MelliMaharat.Application.Common.Interfaces;
using MelliMaharat.Application.Common.Interfaces.Repositories;
using MelliMaharat.Application.Features.Students.Commands.CreateStudent;
using MelliMaharat.Infrastructure.Data;
using MelliMaharat.Infrastructure.Data.Repositories;
using MelliMaharat.Infrastructure.Identity;
using MelliMaharat.Infrastructure.Persistence.Interceptors;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

#region Database
builder.Services.AddScoped<AuditableEntityInterceptor>();

// DbContext
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));


builder.Services.AddScoped<IApplicationDbContext>(provider =>
    provider.GetRequiredService<AppDbContext>());

// Repositories
builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
builder.Services.AddScoped<IStudentRepository, StudentRepository>();

#endregion

#region Identity

// Identity
builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
    .AddEntityFrameworkStores<AppDbContext>()
    .AddDefaultTokenProviders();

builder.Services.ConfigureApplicationCookie(options =>
{
    options.LoginPath = "/Account/Login";
    options.AccessDeniedPath = "/Account/AccessDenied";
});

#endregion

#region Authorization

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("AdminOnly", policy =>
        policy.RequireRole("Admin"));
});

#endregion

#region Application Services
// builder.Services.AddMediatR(...)
// builder.Services.AddValidatorsFromAssembly(...)
#endregion

#region Core Services

builder.Services.AddMediatR(cfg =>
    cfg.RegisterServicesFromAssembly(typeof(CreateStudentCommand).Assembly));

builder.Services.AddValidatorsFromAssembly(typeof(CreateStudentCommand).Assembly);

// MVC
builder.Services.AddControllersWithViews();

#endregion

var app = builder.Build();

#region Middleware

app.UseRouting();

using (var scope = app.Services.CreateScope())
{
    var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
    var userManager = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();

    await IdentitySeeder.SeedRolesAsync(roleManager);
    await IdentitySeeder.SeedAdminUserAsync(userManager);
}

app.UseAuthentication();
app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();

#endregion

#region Development Tools
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}
#endregion

app.Run();
