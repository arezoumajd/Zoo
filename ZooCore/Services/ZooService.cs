using ZooDomain.DataModels;
using ZooDomain.Enums;
using ZooDomain.Services;

namespace ZooCore.Services
{
    public class ZooService : IZooService
    {
        private readonly IParseFileService parseFileService;
        public ZooService(IParseFileService parseFileService)
        {
            this.parseFileService = parseFileService;
        }
        public decimal CalculateTotalCost(string priceFilePath, string animalFilePath, string zooFilePath)
        {
            var prices = parseFileService.ParsePricesFile(priceFilePath);
            var animals = parseFileService.ParseAnimalFile(animalFilePath);
            var zoo = parseFileService.ParseZooFile(zooFilePath, animals.ToList());
            decimal totalcost = 0;
            decimal meatPricePerKg = prices.GetValueOrDefault(((FoodEnum)FoodEnum.Meat).ToString());//("Meat");
            decimal fruitPricePerKg = prices.GetValueOrDefault(((FoodEnum)FoodEnum.Fruit).ToString());//("Fruit");
            foreach (Animal animal in zoo.Animals)
            {
                var foodWeight = (animal.AnimalCategory.RatePerKg * animal.Weight);
                var meatPrice = (animal.AnimalCategory.MeatPercentage * foodWeight * meatPricePerKg) / 100;
                var fruitPrice = ((100 - animal.AnimalCategory.MeatPercentage) * foodWeight * fruitPricePerKg) / 100;

                totalcost += meatPrice + fruitPrice;
            }

            return totalcost;

        }
    }
}