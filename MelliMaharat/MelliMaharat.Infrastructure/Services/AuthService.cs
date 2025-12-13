using Bogus;
using MelliMaharat.Dal.DbContexts;
using MelliMaharat.Dal.UnitOfWork;
using MelliMaharat.Infrastructure.Services.Base;
using MelliMaharat.Models;
using MelliMaharat.UseCases.ViewResult;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace MelliMaharat.Infrastructure.Services
{
    public class AuthService : ServiceBase, IAuthService
    {
        public AuthService(IUnitOfWork unitOfWork) : base(unitOfWork) { }
        public AuthService(ApplicationDbContext context) : base(context) { }

        public async Task<User?> GetUserByUsernameAsync(string username)
        {
            if (unitOfWork is not null)
                return await unitOfWork.Users.GetAll().FirstOrDefaultAsync(u => u.Username == username);
            else if (_context is not null)
                return await _context.Users.FirstOrDefaultAsync(u => u.Username == username);
                
            throw new ArgumentException(nameof(username));
        }

        public async Task<AuthResult> LoginAsync(string username, string password)
        {
            try
            {
                User? user = null;

                if (unitOfWork is not null)
                    user = await unitOfWork.Users.GetAll().FirstOrDefaultAsync(u => u.Username == username);
                else if (_context is not null)
                    user = await _context.Users.Include(x => x.PersonInformation).FirstOrDefaultAsync(u => u.Username == username);   

                if (user == null)
                    return AuthResult.Failure("نام کاربری یا رمز عبور اشتباه است.");

                if (user.Password != password)
                    return AuthResult.Failure("رمز عبور اشتباه است.");
                
                if (_context is not null)
                {
                    switch (user.Role)
                    {
                        case Models.Enums.UserRoles.Admin:
                            return AuthResult.Success(user, "ورود با موفقیت انجام شد.");
                 
                        case Models.Enums.UserRoles.Master:
                            _context.Entry(user).Reference(x => x.Master).Load();
                            if (user.Master is null)
                                return AuthResult.Failure("Master Does Not Exist!");
                            return AuthResult.Success(user, "ورود با موفقیت انجام شد.");

                        case Models.Enums.UserRoles.Student:
                            _context.Entry(user).Reference(x => x.Student).Load();
                            if (user.Student is null)
                                return AuthResult.Failure("Student Does Not Exist!");
                            return AuthResult.Success(user, "ورود با موفقیت انجام شد.");

                        default:
                            return AuthResult.Failure("User Role Undefined!");
                    }
                }
                return AuthResult.Success(user, "ورود با موفقیت انجام شد.");
            }
            catch (Exception ex)
            {
                return AuthResult.Failure($"خطا در فرآیند ورود: {ex.Message}");
            }
            throw new ArgumentException("username" + " " + "password");
        }

        public async Task<AuthResult> ChangePasswordAsync(string username, string currentPassword, string newPassword)
        {
            try
            {
                User? user = default;
                if (unitOfWork is not null)
                    user = await unitOfWork.Users.GetAll().FirstOrDefaultAsync(u => u.Username == username);
                else if (_context is not null)
                    user = await _context.Users.FirstOrDefaultAsync(u => u.Username == username);

                if (user == null)
                    return AuthResult.Failure("کاربر یافت نشد.");

                if (user.Password != currentPassword)
                    return AuthResult.Failure("رمز عبور فعلی اشتباه است.");

                user.Password = newPassword;
                
                if (unitOfWork is not null)
                {
                    unitOfWork.Users.Update(user);
                    await unitOfWork.CommitChangesAsync();
                }
                else if (_context is not null)
                {

                    _context.Users.Update(user);
                    await _context.SaveChangesAsync();
                }
                return AuthResult.Success(user, "رمز عبور با موفقیت تغییر یافت.");
            }
            catch (Exception ex)
            {
                return AuthResult.Failure($"خطا در تغییر رمز عبور: {ex.Message}");
            }
        }
    }
}
