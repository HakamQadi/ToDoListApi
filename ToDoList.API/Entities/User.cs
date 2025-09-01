using System;
using Microsoft.AspNetCore.Identity;

namespace ToDoList.API.Data;

public enum UserRole
{
    User,
    Admin
}

public class User : IdentityUser
{
    // public int Id { get; set; }
    public required string FirstName { get; set; }
    public required string LastName { get; set; }
    public required UserRole Role { get; set; }
    // public required string Email { get; set; }
    // public required string Password { get; set; }
    // public string? Token { get; set; }
    public ICollection<Task> Tasks { get; set; } = new List<Task>();

}
