using Microsoft.Extensions.DependencyInjection;
using ZooCore.Services;
using ZooDomain.Services;

namespace ZooCore
{
    public class Program
    {
        private const string PathPrice = ".\\Files\\prices.txt";
        private const string PathAnimals = ".\\Files\\animals.csv";
        private const string PathZoo = ".\\Files\\zoo.xml";

        public static void Main(string[] args)
        {
            var serviceProvider = new ServiceCollection()
                .AddTransient<IParseFileService, ParseFileService>()
                .AddTransient<IZooService, ZooService>()
                .BuildServiceProvider();

            // var parseFileService = serviceProvider.GetRequiredService<IParseFileService>();
            var zooService = serviceProvider.GetRequiredService<IZooService>();

            var totalcost = zooService.CalculateTotalCost(PathPrice, PathAnimals, PathZoo);
            Console.WriteLine("Total Cost: " + totalcost + " SEK");
        }
    }
}

