using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.Hosting;
using System;

namespace Assignment1
{
    class Program
    {
        static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args)
        {
            return Host.CreateDefaultBuilder(args)
                 .ConfigureWebHostDefaults(webHost =>
                 {
                     webHost.UseStartup<Startup>();
                 });
        }
    }
}
