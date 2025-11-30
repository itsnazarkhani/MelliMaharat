using MelliMaharat.Models;
using MelliMaharat.UseCases.ViewResult;
using System;
using System.Collections.Generic;
using System.Text;

namespace MelliMaharat.Infrastructure.Services
{
    public interface IAuthService
    {
        AuthResult Login(string username, string password);
        User? GetUserByUsername(string username);
    }
}
