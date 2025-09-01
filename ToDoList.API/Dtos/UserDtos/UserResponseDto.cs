using ToDoList.API.Data;

namespace ToDoList.API.Dtos.UserDtos;

public record class UserResponseDto(
    string Id,
    // int Id,
    string FullName,
    UserRole Role,
    string Token
);