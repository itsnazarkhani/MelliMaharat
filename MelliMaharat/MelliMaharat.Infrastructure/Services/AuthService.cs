using MelliMaharat.Dal.UnitOfWork;
using MelliMaharat.Infrastructure.Services.Base;
using MelliMaharat.Models;
using MelliMaharat.UseCases.ViewResult;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

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
                    return AuthResult.Failure("Invalid username or password");
                }
                if (user.Password != password)
                {
                    return AuthResult.Failure("Invalid username or password");
                }
                return AuthResult.Success(user);

            }
            catch (Exception ex)
            {
                return AuthResult.Failure($"An error occurred during login: {ex.Message}");
            }
        }
    }
}
