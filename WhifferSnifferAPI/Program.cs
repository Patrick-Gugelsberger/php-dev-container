using WhifferSnifferAPI.Data;
using Microsoft.EntityFrameworkCore;
using WhifferSnifferAPI.IService;
using WhifferSnifferAPI.Service;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Hosting;
using Microsoft.OpenApi.Models;
using Microsoft.AspNetCore.Authentication;
using WhifferSnifferAPI;

#nullable disable

public class Program
{

    public static void Main(string[] args)
    {
        CreateHostBuilder(args).Build().Run();
    }

    public static IHostBuilder CreateHostBuilder(string[] args) =>
        Host.CreateDefaultBuilder(args)
            .ConfigureWebHostDefaults(webBuilder =>
            {
                webBuilder.UseStartup<Startup>();
            });
}

