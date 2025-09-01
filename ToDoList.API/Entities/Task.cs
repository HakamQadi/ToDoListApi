using System;

namespace ToDoList.API.Data;

public enum TaskStatus
{
    Todo,
    Pending,
    Done
}
public class Task
{
    public int Id { get; set; }
    public required string Title { get; set; }
    public string? Description { get; set; }
    public required TaskStatus Status { get; set; }
    public int UserId { get; set; }

    public required User User { get; set; }
}
