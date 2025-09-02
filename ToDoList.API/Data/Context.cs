using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ToDoList.API.Data;

public class Context : IdentityDbContext<User>
{
    public Context(DbContextOptions<Context> options) : base(options) { }

    public DbSet<Task> Tasks { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder); // Important: call base for Identity

        modelBuilder.Entity<User>(b =>
        {
            // Rename Identity tables
            b.ToTable("Users");

            //These columns needed for login
            // b.Ignore(u => u.UserName);
            // b.Ignore(u => u.NormalizedUserName);
            // b.Ignore(u => u.NormalizedEmail);

            // Ignore columns you don't need
            b.Ignore(u => u.EmailConfirmed);
            b.Ignore(u => u.PhoneNumber);
            b.Ignore(u => u.PhoneNumberConfirmed);
            b.Ignore(u => u.TwoFactorEnabled);
            b.Ignore(u => u.LockoutEnd);
            b.Ignore(u => u.LockoutEnabled);
            b.Ignore(u => u.AccessFailedCount);
            b.Ignore(u => u.ConcurrencyStamp);
            b.Ignore(u => u.SecurityStamp);
        });

        // Store enums as strings
        modelBuilder.Entity<User>().Property(u => u.Role).HasConversion<string>();
        modelBuilder.Entity<Task>().Property(t => t.Status).HasConversion<string>();

        // Configure one-to-many relationship between User and Task
        modelBuilder.Entity<User>()
            .HasMany(u => u.Tasks)
            .WithOne(t => t.User)
            .HasForeignKey(t => t.UserId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
