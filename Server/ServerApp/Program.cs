using System;
using System.IO;
using System.Net;
using Microsoft.AspNetCore.Hosting;

namespace ServerApp
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                Console.WriteLine("Server Starting...");
      
                var host = new WebHostBuilder()
                    .UseKestrel()
                    .UseContentRoot(Directory.GetCurrentDirectory())
                    .UseIISIntegration()
                    .UseStartup<Startup>()
                    .UseKestrel(options =>
                    {
                        options.Listen(IPAddress.Any, 5002);
                    })
                    .Build();

                host.Run();

                Console.WriteLine("Server Started");

            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }
    }
}
