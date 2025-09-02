using System.ComponentModel.DataAnnotations;

namespace ToDoList.API.Dtos.TaskDtos;

public record class UpdateTaskDto(
    [Required][MaxLength(100)] string Title,
    [Required][MaxLength(500)] string Description,
    // [Required] TaskStatus  Status
    [Required] ToDoList.API.Data.TaskStatus Status
);
