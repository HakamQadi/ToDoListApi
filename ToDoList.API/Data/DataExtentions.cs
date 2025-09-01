// using System;

// namespace ToDoList.API.Data;

// public class DataExtentions
// {
//     public static void AddDatabase(this IServiceCollection services, IConfiguration config)
//     {
//         var connectionString = config.GetConnectionString("DefaultConnection");

//         services.AddDbContext<FootballContext>(options =>
//             options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)));
//     }
// }
