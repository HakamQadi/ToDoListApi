namespace ToDoList.API.Dtos.TaskDtos;

public record class TaskResponseDto(
    int Id,
    string Title,
    string Description,
    TaskStatus Status,
    // int UserId,
    string UserId,
    string UserFullName
);
