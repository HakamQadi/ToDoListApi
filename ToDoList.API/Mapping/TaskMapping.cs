using ToDoList.API.Dtos.TaskDtos;

namespace ToDoList.API.Mapping;

public static class TaskMapping
{
    public static Data.Task ToEntity(this CreateTaskDto dto, string userId)
    {
        return new Data.Task()
        {
            Title = dto.Title,
            Description = dto.Description,
            Status = dto.Status,
            UserId = userId
        };
    }

    public static void ToUpdateEntity(this Data.Task task, UpdateTaskDto dto, string userId)
    {

        task.Title = dto.Title;
        task.Description = dto.Description;
        task.Status = (Data.TaskStatus)dto.Status;
        task.UserId = userId;
    }

    public static TaskResponseDto ToResponseDto(this Data.Task task)
    {
        return new TaskResponseDto(
            Id: task.Id,
            Title: task.Title,
            Description: task.Description ?? string.Empty,
            Status: (TaskStatus)task.Status,
            UserId: task.UserId,
            UserFullName: task.User is not null
            ? $"{task.User.FirstName} {task.User.LastName}"
            : string.Empty
        );


    }

}
