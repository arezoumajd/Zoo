using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Moq;
using ZooCore.Services;
using ZooDomain.DataModels;
using ZooDomain.Enums;
using ZooDomain.Services;

namespace ZooTest
{
    public class TestZooService
    {
        [Fact]
        public void CalculateTotalCost_ShouldCalculateCorrectTotalCost()
        {
            // Arrange
            string priceFilePath = "../../../../ZooCore/Files/prices.txt";
            string animalFilePath = "../../../../ZooCore/Files//animals.csv";
            string zooFilePath = "../../../../ZooCore/Files/zoo.xml";

            var parseFileServiceMock = new Mock<IParseFileService>();

            var prices = new Dictionary<string, decimal> {
                { "Meat", 12.56m },
                { "Fruit", 5.60m }
            };

            var animalCategories = new List<AnimalCategory> {
                new AnimalCategory { Name = "Lion", RatePerKg = 0.10m, Type = AnimalTypeEnum.Carnivores, MeatPercentage = 100 },
                new AnimalCategory { Name = "Giraffe", RatePerKg = 0.08m, Type = AnimalTypeEnum.Herbivores, MeatPercentage = 0 },
            };

            var zoo = new Zoo
            {
                Animals = new List<Animal> {
                    new Animal { Name = "Simba", Weight = 160m, AnimalCategory = animalCategories[0] },
                    new Animal { Name = "Hanna", Weight = 200m, AnimalCategory = animalCategories[1] }
                }
            };

            parseFileServiceMock.Setup(pfs => pfs.ParsePricesFile(priceFilePath)).Returns(prices);

            parseFileServiceMock.Setup(pfs => pfs.ParseAnimalFile(animalFilePath)).Returns(animalCategories);

            parseFileServiceMock.Setup(pfs => pfs.ParseZooFile(zooFilePath, animalCategories)).Returns(zoo);

            var zooService = new ZooService(parseFileServiceMock.Object);

            // Act
            decimal totalCost = zooService.CalculateTotalCost(priceFilePath, animalFilePath, zooFilePath);

            // Assert
            Assert.Equal(290.56m, totalCost);
            parseFileServiceMock.Verify(pfs => pfs.ParsePricesFile(priceFilePath), Times.Once);
            parseFileServiceMock.Verify(pfs => pfs.ParseAnimalFile(animalFilePath), Times.Once);
            parseFileServiceMock.Verify(pfs => pfs.ParseZooFile(zooFilePath, animalCategories), Times.Once);
        }
    }
}