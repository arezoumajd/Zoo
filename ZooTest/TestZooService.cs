using Xunit;
using Moq;
using System.Collections.Generic;
using ZooDomain.Services;
using ZooDomain.Enums;
using ZooDomain.DataModels;
using ZooCore.Services;

public class ZooServiceTests
{
    [Fact]
    public void CalculateTotalCost_ShouldCalculateCorrectTotalCost()
    {
        // Arrange
        var parseFileServiceMock = new Mock<IParseFileService>();
        var zooServiceMock = new Mock<IZooService>();

        // Create test data for food prices
        var foodPrices = new Dictionary<string, decimal>
        {
            { ((FoodEnum)FoodEnum.Meat).ToString(), 12.56m },
            { ((FoodEnum)FoodEnum.Fruit).ToString(), 5.60m }
        };

        // Create test data for the zoo
        var zoo = new Zoo
        {
            Animals = new List<Animal>
            {
                new Animal { Name = "Simba", Weight = 160m, AnimalCategory = new AnimalCategory { RatePerKg = 0.10m, MeatPercentage = 100 } },
                new Animal { Name = "Hanna", Weight = 200m, AnimalCategory = new AnimalCategory { RatePerKg = 0.08m, MeatPercentage = 0 } },
            }
        };

        parseFileServiceMock.Setup(pfs => pfs.GetFoodPrices()).Returns(foodPrices);

        parseFileServiceMock.Setup(pfs => pfs.GetZoo()).Returns(zoo);

        var zooService = new ZooService(parseFileServiceMock.Object);

        // Act
        decimal totalCost = zooService.CalculateTotalCost();

        // Assert
        Assert.Equal(290.56m, totalCost); // Adjust the expected value based on your calculation

        // Verify that the mock methods were called
        parseFileServiceMock.Verify(pfs => pfs.GetFoodPrices(), Times.Once);
        parseFileServiceMock.Verify(pfs => pfs.GetZoo(), Times.Once);
    }
}
