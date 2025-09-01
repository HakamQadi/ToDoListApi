using System;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ToDoList.API.Data;
using ToDoList.API.Dtos.UserDtos;
using ToDoList.API.Services;

namespace ToDoList.API.Endpoints;

public static class UserEndpoints
{
    public static RouteGroupBuilder MapUserEndpoints(this WebApplication app)
    {
        var group = app.MapGroup("api/users").WithParameterValidation();

        group.MapPost("/register", async (
           UserRegisterDto newUser,
           Context db,
           UserManager<User> userManager,
           JwtService jwtService) =>
       {
           if (await db.Users.AnyAsync(u => u.Email == newUser.Email))
               return Results.BadRequest("Email already exists.");

           var user = new User
           {
               FirstName = newUser.FirstName,
               LastName = newUser.LastName,
               Email = newUser.Email,
               UserName = newUser.Email,
               Role = newUser.Role
           };

           var result = await userManager.CreateAsync(user, newUser.Password);
           if (!result.Succeeded) return Results.BadRequest(result.Errors);

           var token = jwtService.GenerateToken(user);

           return Results.Ok(new UserResponseDto(user.Id, $"{user.FirstName} {user.LastName}", user.Role, token));
       });

        group.MapPost("/login", async (
            UserLoginDto login,
            UserManager<User> userManager,
            JwtService jwtService) =>
        {
            var user = await userManager.FindByEmailAsync(login.Email);
            if (user == null) return Results.BadRequest("Invalid credentials.");

            var valid = await userManager.CheckPasswordAsync(user, login.Password);
            if (!valid) return Results.BadRequest("Invalid credentials.");

            var token = jwtService.GenerateToken(user);

            return Results.Ok(new UserResponseDto(user.Id, $"{user.FirstName} {user.LastName}", user.Role, token));
        });


        return group;
    }

}
