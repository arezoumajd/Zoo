using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using ZooCore.Services;
using ZooDomain.DTO;
using ZooDomain.Services;

namespace ZooCore
{
    public class Program
    {
        public static void Main(string[] args)
        {
            string path = ".\\Files\\";
            if (args.Length > 0 && args[0].Length > 0)
            {
                path = Path.GetFullPath(args[0].Trim('"'));
                Console.WriteLine("trying path: " + path);
                if (Directory.Exists(path))
                {
                    var files = Directory.GetFiles(path);
                    foreach (var file in files) Console.WriteLine(file);
                }
                else
                {
                    Console.WriteLine("path doesn't exist");
                    return;
                }
            }

            string pricesFilePath = Path.Combine(path, "prices.txt");
            string animalsFilePath = Path.Combine(path, "animals.csv");
            string zooFilePath = Path.Combine(path, "zoo.xml");

            var serviceProvider = ConfigureServiceProvider(pricesFilePath, animalsFilePath, zooFilePath);

            var loggerFactory = serviceProvider.GetRequiredService<ILoggerFactory>();

            var logger = loggerFactory.CreateLogger<Program>();
            logger.LogInformation("Application starting...");

            var app = serviceProvider.GetRequiredService<ZooApplication>();

            app.Run();

            Console.WriteLine("Press ESC to close...");
            while (true)
            {
                if (Console.ReadKey(true).Key == ConsoleKey.Escape)
                {
                    if (app is IDisposable disposableApp)
                    {
                        disposableApp.Dispose();
                    }
                    break;
                }
            }

        }

        private static IServiceProvider ConfigureServiceProvider(string pricesFilePath, string animalsFilePath, string zooFilePath)
        {
            var services = new ServiceCollection();

            services.AddTransient<IParseFileService, ParseFileService>();
            services.AddTransient<IZooService, ZooService>();
            services.AddTransient<ZooApplication>();

            services.AddSingleton(new FilePathsDto
            {
                PricesFilePath = pricesFilePath,
                AnimalsFilePath = animalsFilePath,
                ZooFilePath = zooFilePath
            });

            services.AddLogging(logging =>
            {
                logging.ClearProviders();
                logging.AddConsole();
            });

            return services.BuildServiceProvider();
        }
    }
}

