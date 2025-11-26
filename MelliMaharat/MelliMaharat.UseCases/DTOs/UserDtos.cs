using System;
using System.Collections.Generic;
using System.Text;

namespace MelliMaharat.UseCases.DTOs
{
    public record class LoginDto (string Username, string Password);

    public record class UserDto
    (
        Guid Id,
        string FirstName,
        string LastName,
        int Age,
        string NationalCode,
        string PhoneNumber,
        string Email,
        string Username,
        bool IsAdmin,
        string Role
    );
}
