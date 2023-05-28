using ZooDomain.DTO;
using ZooDomain.Services;

namespace ZooCore
{
    public class ZooApplication
    {
        private readonly IParseFileService parseFileService;
        private readonly IZooService zooService;
        private readonly FilePathsDto filePaths;

        public ZooApplication(IParseFileService parseFileService, IZooService zooService, FilePathsDto filePaths)
        {
            this.parseFileService = parseFileService;
            this.zooService = zooService;
            this.filePaths = filePaths;
        }

        public void Run()
        {
            var prices = parseFileService.ParsePricesFile(filePaths.PricesFilePath);
            var animals = parseFileService.ParseAnimalFile(filePaths.AnimalsFilePath);
            var zoo = parseFileService.ParseZooFile(filePaths.ZooFilePath, animals);

            var totalCost = zooService.CalculateTotalCost(new CalculateDto
            {
                FoodPrices = prices,
                ZooDetails = zoo
            });
            Console.WriteLine("TotalCost: " + totalCost + " SEK");
        }
    }
}