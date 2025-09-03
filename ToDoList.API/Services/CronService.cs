using System;

namespace ToDoList.API.Services;

public class CronService
{
    public void SelfPing()
    {
        var client = new HttpClient();
        var uri = "https://todolistapi-4w15.onrender.com";

        // Ping every 10 minutes
        _ = System.Threading.Tasks.Task.Run(async () =>
        {
            while (true)
            {
                try
                {
                    await client.GetAsync(uri);
                    Console.WriteLine($"Pinged at {DateTime.Now}");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Ping failed: {ex.Message}");
                }
                // await System.Threading.Tasks.Task.Delay(TimeSpan.FromSeconds(10));
                await System.Threading.Tasks.Task.Delay(TimeSpan.FromMinutes(14));
            }
        });
    }

}
