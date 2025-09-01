using System;
using Microsoft.EntityFrameworkCore;

namespace ToDoList.API.Data;

public static class DataExtentions
{
    public static void AddDatabase(this IServiceCollection services, IConfiguration config)
    {
        var connectionString = config.GetConnectionString("DefaultConnection");

        services.AddDbContext<Context>(options =>
            options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)));
    }
}
