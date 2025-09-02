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
    public required string UserId { get; set; }

    public User? User { get; set; }
}
