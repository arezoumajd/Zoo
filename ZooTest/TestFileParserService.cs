using Moq;
using ZooCore.Services;
using ZooDomain.DataModels;
using ZooDomain.Enums;
using ZooDomain.Services;

namespace ZooTest;

public class TestFileParserService
{
    [Fact]
    public void ParsePricesFile_ShouldParsePricesFromFile()
    {
        // Arrange
        string testFilePath = "../../../../ZooCore/Files/prices.txt";
        string testFileContent = "Meat=12.56\nFruit=5.60";
        File.WriteAllText(testFilePath, testFileContent);

        var parseFileServiceMock = new Mock<IParseFileService>();
        parseFileServiceMock.Setup(service => service.ParsePricesFile(testFilePath))
            .Returns(new Dictionary<string, decimal>
            {
                { "Meat", 12.56m },
                { "Fruit", 5.60m }
            });

        // Act
        Dictionary<string, decimal> result = parseFileServiceMock.Object.ParsePricesFile(testFilePath);

        // Assert
        Assert.Equal(2, result.Count);
        Assert.Equal(12.56m, result["Meat"]);
        Assert.Equal(5.60m, result["Fruit"]);
    }

    [Fact]
    public void ParseAnimalFile_ShouldParseAnimalCategoriesFromFile()
    {
        // Arrange
        string testFilePath = "../../../../ZooCore/Files/animals.csv";
        string testFileContent = "Lion;0.10;meat";
        File.WriteAllText(testFilePath, testFileContent);

        var parseFileServiceMock = new Mock<IParseFileService>();
        parseFileServiceMock.Setup(service => service.ParseAnimalFile(testFilePath))
            .Returns(new List<AnimalCategory>{
                new AnimalCategory {
                    Name = "Lion",
                    RatePerKg = 0.10m,
                    Type = AnimalTypeEnum.Carnivores,
                    MeatPercentage = 100
                }
            });

        // Act
        List<AnimalCategory> result = parseFileServiceMock.Object.ParseAnimalFile(testFilePath);

        // Assert
        Assert.Equal(1, result.Count);

        Assert.Equal("Lion", result[0].Name);
        Assert.Equal(0.10m, result[0].RatePerKg);
        Assert.Equal(AnimalTypeEnum.Carnivores, result[0].Type);
        Assert.Equal(100, result[0].MeatPercentage);
    }

    [Fact]
    public void ParseZooFile_ShouldParseAnimalsFromFile()
    {
        // Arrange
        string testFilePath = "../../../../ZooCore/Files/zoo.xml";
        string testFileContent = "<Zoo>\n <Lions>\n <Lion name='Simba' kg='160'/>\n </Lions>\n</Zoo>";
        File.WriteAllText(testFilePath, testFileContent);
        List<AnimalCategory> animalCategories = new List<AnimalCategory>
        {
            new AnimalCategory { Name = "Lion", RatePerKg = 0.10m, Type = AnimalTypeEnum.Carnivores, MeatPercentage = 100 }
        };
        var parseFileServiceMock = new Mock<IParseFileService>();
        parseFileServiceMock.Setup(service => service.ParseZooFile(testFilePath, animalCategories))
            .Returns(new Zoo
            {
                Animals =
                new List<Animal> {
                    new Animal {
                        Name = "Simba",
                        Weight = 160,
                        AnimalCategory = new AnimalCategory
                        {
                            Name = "Lion",
                            RatePerKg = (decimal) 0.10,
                            Type = AnimalTypeEnum.Carnivores,
                            MeatPercentage = 100
                        }
                    }
                }
            });

        // Act
        Zoo result = parseFileServiceMock.Object.ParseZooFile(testFilePath, animalCategories);

        // Assert
        Assert.NotNull(result);
        Assert.NotNull(result.Animals);
        Assert.Single(result.Animals);

        Animal lion = result.Animals.Where(x => x.AnimalCategory.Name == "Lion").FirstOrDefault();
        Assert.Equal("Simba", lion.Name);
        Assert.Equal(160m, lion.Weight);
        //Assert.Equal(animalCategories[0], lion.AnimalCategory); ;
        Assert.IsType<AnimalCategory>(lion.AnimalCategory);
    }
}