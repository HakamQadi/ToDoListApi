using System;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ToDoList.API.Data;
using ToDoList.API.Dtos.TaskDtos;
using ToDoList.API.Mapping;

namespace ToDoList.API.Endpoints;

public static class TaskEndpoints
{
    public static RouteGroupBuilder MapTaskEndpoints(this WebApplication app)
    {
        var group = app.MapGroup("api/tasks").RequireAuthorization().WithParameterValidation();

        group.MapGet("/", async (Context db, HttpContext httpContext) =>
        {
            var userId = httpContext.User.FindFirst("id")?.Value;
            if (userId is null)
            {
                return Results.Unauthorized();
            }

            var tasks = await db.Tasks
                .Include(t => t.User)
                .Where(t => t.UserId == userId)
                .Select(t => t.ToResponseDto())
                .ToListAsync();

            return Results.Ok(tasks);
        });

        group.MapGet("/{id}", async (int id, Context db, HttpContext httpContext) =>
        {
            var userId = httpContext.User.FindFirst("id")?.Value;
            if (userId is null)
            {
                return Results.Unauthorized();
            }

            var task = await db.Tasks
            .Include(t => t.UserId)
            .FirstOrDefaultAsync(t => t.Id == id && t.UserId == userId);

            return task is null
            ? Results.NotFound("Task not found")
            : Results.Ok(task.ToResponseDto());
        });

        group.MapPost("/create", async (CreateTaskDto dto,
            Context db,
            UserManager<User> UserManager,
            HttpContext httpContext) =>
        {
            var userId = httpContext.User.FindFirst("id")?.Value;

            if (userId is null)
            {
                return Results.Unauthorized();
            }

            var task = dto.ToEntity(userId);
            db.Tasks.Add(task);
            await db.SaveChangesAsync();

            return Results.Ok(task.ToResponseDto());
        });

        group.MapPatch("/update/{id}", async (int id,
        UpdateTaskDto dto,
        Context db,
        HttpContext httpContext) =>
        {
            var userId = httpContext.User.FindFirst("id")?.Value;
            if (userId is null)
            {
                return Results.Unauthorized();
            }

            var task = await db.Tasks.FirstOrDefaultAsync(t => t.Id == id && t.UserId == userId);
            if (task is null)
            {
                return Results.NotFound("Task not found");
            }

            task.ToUpdateEntity(dto, userId);
            await db.SaveChangesAsync();


            return Results.Ok(task.ToResponseDto());
        });

        group.MapDelete("/delete/{id}", async (int id, Context db, HttpContext httpContext) =>
        {
            var userId = httpContext.User.FindFirst("id")?.Value;
            if (userId is null)
            {
                return Results.Unauthorized();
            }

            var task = await db.Tasks.FirstOrDefaultAsync(t => t.Id == id && t.UserId == userId);
            if (task is null)
            {
                return Results.NotFound("Task not found");
            }

            // Marks the entity as Deleted in the EF Core change tracker.
            // On SaveChangesAsync(), EF generates a SQL DELETE statement for that specific entity.

            // When to use:
            // You might want EF to track the entity before deletion (e.g., to trigger cascade deletes or EF events).

            db.Tasks.Remove(task);
            await db.SaveChangesAsync();

            // Generates a direct SQL DELETE based on the query.
            // Does NOT load the entities into memory.

            // When to use:
            // You just want to delete matching rows directly in the database.
            // You don’t need EF tracking or events.

            // await db.Tasks.ExecuteDeleteAsync();

            return Results.NoContent();
        });
        return group;
    }
}
