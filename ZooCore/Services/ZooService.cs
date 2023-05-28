using ZooDomain.DataModels;
using ZooDomain.DTO;
using ZooDomain.Enums;
using ZooDomain.Services;

namespace ZooCore.Services
{
    public class ZooService : IZooService
    {
        public decimal CalculateTotalCost(CalculateDto dto)
        {
            decimal totalcost = 0;
            decimal meatPricePerKg = dto.FoodPrices.GetValueOrDefault(((FoodEnum)FoodEnum.Meat).ToString());
            decimal fruitPricePerKg = dto.FoodPrices.GetValueOrDefault(((FoodEnum)FoodEnum.Fruit).ToString());
            foreach (Animal animal in dto.ZooDetails.Animals)
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