using MelliMaharat.Models;

namespace MelliMaharat.UseCases.ViewResult
{
    public class AuthResult
    {
        public bool IsSuccess { get; set; }
        public string Message { get; set; } = string.Empty;
        public User? User { get; set; }

        public static AuthResult Success(User user, string message = "ورود با موفقیت انجام شد.") =>
            new() { IsSuccess = true, User = user, Message = message };

        public static AuthResult Failure(string message = "خطایی رخ داده است.") =>
            new() { IsSuccess = false, Message = message, User = null! };
    }
}
