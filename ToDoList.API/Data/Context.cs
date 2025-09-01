using System;
using Microsoft.EntityFrameworkCore;

namespace ToDoList.API.Data;

public class Context(DbContextOptions<Context> options) : DbContext(options)
{
    public DbSet<User> Users => Set<User>();
    public DbSet<Task> Tasks => Set<Task>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Store enums as strings
        modelBuilder.Entity<User>().Property(u => u.Role).HasConversion<string>();
        modelBuilder.Entity<Task>().Property(t => t.Status).HasConversion<string>();

        // Configure one-to-many relationship
        modelBuilder.Entity<User>()
        .HasMany(u => u.Tasks)
        .WithOne(t => t.User)
        .HasForeignKey(t => t.UserId)
        .OnDelete(DeleteBehavior.Cascade);
    }

}
