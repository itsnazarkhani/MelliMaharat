using MelliMaharat.Dal.UnitOfWork;
using MelliMaharat.Infrastructure.Services.Base;
using MelliMaharat.Models;
using MelliMaharat.UseCases.ViewResult;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MelliMaharat.Infrastructure.Services
{
    public class AuthService : ServiceBase, IAuthService
    {
        public AuthService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        public async Task<User?> GetUserByUsernameAsync(string username) =>
                       await unitOfWork.Users.GetAll().FirstOrDefaultAsync(u => u.Username == username);

        public async Task<AuthResult> LoginAsync(string username, string password)
        {
            try
            {
                var user = await unitOfWork.Users.GetAll().FirstOrDefaultAsync(u => u.Username == username);
                if (user == null)
                {
                    return AuthResult.Failure("نام کاربری یا رمز عبور اشتباه است.");
                }
                if (user.Password != password)
                {
                    return AuthResult.Failure("نام کاربری یا رمز عبور اشتباه است.");
                }
                return AuthResult.Success(user, "ورود با موفقیت انجام شد.");
            }
            catch (Exception ex)
            {
                return AuthResult.Failure($"خطا در فرآیند ورود: {ex.Message}");
            }
        }

        public async Task<AuthResult> ChangePasswordAsync(string username, string currentPassword, string newPassword)
        {
            try
            {
                var user = await unitOfWork.Users.GetAll().FirstOrDefaultAsync(u => u.Username == username);
                if (user == null)
                    return AuthResult.Failure("کاربر یافت نشد.");

                if (user.Password != currentPassword)
                    return AuthResult.Failure("رمز عبور فعلی اشتباه است.");

                user.Password = newPassword;
                unitOfWork.Users.Update(user);
                await unitOfWork.CommitChangesAsync();

                return AuthResult.Success(user, "رمز عبور با موفقیت تغییر یافت.");
            }
            catch (Exception ex)
            {
                return AuthResult.Failure($"خطا در تغییر رمز عبور: {ex.Message}");
            }
        }
    }
}
