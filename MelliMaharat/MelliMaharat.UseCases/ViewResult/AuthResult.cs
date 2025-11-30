using MelliMaharat.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace MelliMaharat.UseCases.ViewResult
{
    public class AuthResult
    {
        public bool IsSuccess { get; set; }
        public string Message { get; set; } = string.Empty;
        public User? User { get; set; }

        public static AuthResult Success(User user) =>
            new() { IsSuccess = true, User = user, Message = "Login successful" };

        public static AuthResult Failure(string message) =>
            new() { IsSuccess = false, Message = message, User = null! };
    }
}
