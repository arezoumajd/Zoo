using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Moq;
using ZooCore.Services;
using ZooDomain.DataModels;
using ZooDomain.DTO;
using ZooDomain.Enums;
using ZooDomain.Services;

namespace ZooTest
{
    public class TestZooService
    {
        [Fact]
        public void CalculateTotalCost_ShouldCalculateTotalCost()
        {
            // Arrange
            var dto = new CalculateDto
            {
                FoodPrices = new Dictionary<string, decimal>
                {
                    { "Meat", 12.56m },
                    { "Fruit", 5.60m }
                },
                ZooDetails = new Zoo
                {
                    Animals = new List<Animal>
                    {
                        new Animal
                        {
                            AnimalCategory = new AnimalCategory
                            {
                                RatePerKg = 8.5m,
                                MeatPercentage = 70,
                                Type = AnimalTypeEnum.Omnivores
                            },
                            Weight = 120
                        },
                        new Animal
                        {
                            AnimalCategory = new AnimalCategory
                            {
                                RatePerKg = 6.25m,
                                MeatPercentage = 40,
                                Type = AnimalTypeEnum.Omnivores
                            },
                            Weight = 85
                        }
                    }
                }
            };

            var zooService = new ZooService();

            // Act
            decimal totalCost = zooService.CalculateTotalCost(dto);

            // Assert
            decimal expectedCost = (((8.5m * 120 * 70 * 12.56m) + (8.5m * 120 * 30 * 5.60m)) / 100) + (((6.25m * 85 * 40 * 12.56m) + (6.25m * 85 * 60 * 5.60m)) / 100);
            Assert.Equal(expectedCost, totalCost);
        }
    }
}