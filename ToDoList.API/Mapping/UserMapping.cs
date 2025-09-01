using System;
using Microsoft.AspNetCore.Identity;
using ToDoList.API.Data;
using ToDoList.API.Dtos.UserDtos;


namespace ToDoList.API.Mapping;

public static class UserMapping
{
    private static readonly PasswordHasher<User> _passwordHasher = new();
    public static User ToEntity(this UserRegisterDto dto)
    {
        var user = new User()
        {
            FirstName = dto.FirstName,
            LastName = dto.LastName,
            Email = dto.Email,
            UserName = dto.Email,
            Role = dto.Role,
        };
        user.PasswordHash = _passwordHasher.HashPassword(user, dto.Password);
        return user;
    }

    public static bool VerifyPassword(this User user, string password)
    {
        var result = _passwordHasher.VerifyHashedPassword(user, user.PasswordHash, password);
        return result == PasswordVerificationResult.Success;
    }

    public static UserResponseDto ToResponseDto(this User user, string token)
    {
        return new UserResponseDto(
            Id: user.Id,
            FullName: $"{user.FirstName} {user.LastName}",
            Role: user.Role,
            Token: token
        );
    }

}
