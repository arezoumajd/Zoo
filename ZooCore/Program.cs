using Microsoft.Extensions.DependencyInjection;
using ZooCore.Services;
using ZooDomain.DTO;
using ZooDomain.Services;

namespace ZooCore
{
    public class Program
    {
        private const string PathPrice = ".\\ZooCore\\Files\\prices.txt";
        private const string PathAnimals = ".\\ZooCore\\Files\\animals.csv";
        private const string PathZoo = ".\\ZooCore\\Files\\zoo.xml";

        public static void Main(string[] args)
        {
            var serviceProvider = new ServiceCollection()
                .AddTransient<IParseFileService, ParseFileService>()
                .AddTransient<IZooService, ZooService>()
                .BuildServiceProvider();

            var parseFileService = serviceProvider.GetRequiredService<IParseFileService>();
            var zooService = serviceProvider.GetRequiredService<IZooService>();

            FilePaths paths = new FilePaths
            {
                PricesFilePath = PathPrice,
                AnimalsFilePath = PathAnimals,
                ZooFilePath = PathZoo
            };

            if (parseFileService.ParseAllFiles(paths))
            {
                var totalcost = zooService.CalculateTotalCost();
                Console.WriteLine("Total Cost: " + totalcost + " SEK");
            }
        }
    }
}

