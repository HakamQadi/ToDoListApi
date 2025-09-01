using System.ComponentModel.DataAnnotations;
using ToDoList.API.Data;
using TaskStatus = ToDoList.API.Data.TaskStatus;

namespace ToDoList.API.Dtos.TaskDtos;

public record class CreateTaskDto
(
    [Required][MaxLength(100)] string Title,
    [MaxLength(500)] string Description,
    [Required] TaskStatus Status
);
