using System.ComponentModel.DataAnnotations;
using ToDoList.API.Data;

namespace ToDoList.API.Dtos.UserDtos;

public record class UserRegisterDto
(
    [Required] string FirstName,
    [Required] string LastName,
    [Required][EmailAddress] string Email,
    [Required]
    [MinLength(8)]
    [RegularExpression(
        @"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[\W_]).+$",
        ErrorMessage = "Password must contain at least one uppercase letter, one lowercase letter, one number, and one special character"
    )] string Password,
    [Required] UserRole Role
);
