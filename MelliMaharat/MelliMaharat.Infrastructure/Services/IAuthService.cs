using MelliMaharat.Models;
using MelliMaharat.UseCases.ViewResult;
using System;
using System.Collections.Generic;
using System.Text;

namespace MelliMaharat.Infrastructure.Services
{
    public interface IAuthService
    {
        Task<AuthResult> LoginAsync(string username, string password);
        Task<User?> GetUserByUsernameAsync(string username);
    }
}
