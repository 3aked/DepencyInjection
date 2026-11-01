using DepencyInjection.Extensions;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Hosting;


IHost? host = null; 
try
{
    Console.WriteLine($"[STARTUP] Building host.....");
    host = Host.CreateDefaultBuilder(args)
        .AddJsonConfiguration()
        .RegistreDI()
        .Build();
    Console.WriteLine($"[STARTUP] Building host successfuly.");

    await host.RunAsync();
}
catch (Exception ex)
{
    Console.WriteLine($"[STARTUP ERROR] { ex.GetType()}: {ex.Message}.");
}
