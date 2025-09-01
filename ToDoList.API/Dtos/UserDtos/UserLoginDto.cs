using System.ComponentModel.DataAnnotations;

namespace ToDoList.API.Dtos.UserDtos;

public record class UserLoginDto(
    [Required][EmailAddress] string Email,
    [Required] string Password
);
