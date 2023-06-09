using Moq;
using ZooCore.Services;
using ZooDomain.DataModels;
using ZooDomain.Enums;
using ZooDomain.Services;

namespace ZooTest;

public class TestFileParserService
{
    [Theory]
    [InlineData("Meat=12.56\nFruit=5.60")]
    public void ParsePricesFile_ShouldParsePricesFromFile(string content)
    {
        // Arrange
        string path = ".\\prices.txt";
        File.WriteAllText(path, content);

        var parseFileServiceMock = new Mock<IParseFileService>();
        parseFileServiceMock.Setup(service => service.ParsePricesFile(path))
            .Returns(new Dictionary<string, decimal>
            {
                { "Meat", 12.56m },
                { "Fruit", 5.60m }
            });

        // Act
        Dictionary<string, decimal> result = parseFileServiceMock.Object.ParsePricesFile(path);

        // Assert
        Assert.Equal(2, result.Count);
        Assert.Equal(12.56m, result["Meat"]);
        Assert.Equal(5.60m, result["Fruit"]);
    }

    [Theory]
    [InlineData("Lion;0.10;meat")]
    public void ParseAnimalFile_ShouldParseAnimalCategoriesFromFile(string content)
    {
        // Arrange
        string path = ".\\animals.csv";
        File.WriteAllText(path, content);

        var parseFileServiceMock = new Mock<IParseFileService>();
        parseFileServiceMock.Setup(service => service.ParseAnimalFile(path))
            .Returns(new List<AnimalCategory>{
                new AnimalCategory {
                    Name = "Lion",
                    RatePerKg = 0.10m,
                    Type = AnimalTypeEnum.Carnivores,
                    MeatPercentage = 100
                }
            });

        // Act
        List<AnimalCategory> result = parseFileServiceMock.Object.ParseAnimalFile(path);

        // Assert
        Assert.Equal(1, result.Count);

        Assert.Equal("Lion", result[0].Name);
        Assert.Equal(0.10m, result[0].RatePerKg);
        Assert.Equal(AnimalTypeEnum.Carnivores, result[0].Type);
        Assert.Equal(100, result[0].MeatPercentage);
    }

    [Theory]
    [InlineData("<Zoo>\n <Lions>\n <Lion name='Simba' kg='160'/>\n </Lions>\n</Zoo>")]
    public void ParseZooFile_ShouldParseAnimalsFromFile(string content)
    {
        // Arrange
        string path = ".\\zoo.xml";
        File.WriteAllText(path, content);
        List<AnimalCategory> animalCategories = new List<AnimalCategory>
        {
            new AnimalCategory { Name = "Lion", RatePerKg = 0.10m, Type = AnimalTypeEnum.Carnivores, MeatPercentage = 100 }
        };
        var parseFileServiceMock = new Mock<IParseFileService>();
        parseFileServiceMock.Setup(service => service.ParseZooFile(path, animalCategories))
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
        Zoo result = parseFileServiceMock.Object.ParseZooFile(path, animalCategories);

        // Assert
        Assert.NotNull(result);
        Assert.NotNull(result.Animals);
        Assert.Single(result.Animals);

        Animal? lion = result.Animals.Where(x => x.AnimalCategory.Name == "Lion").FirstOrDefault();
        Assert.Equal("Simba", lion?.Name);
        Assert.Equal(160m, lion?.Weight);
        Assert.IsType<AnimalCategory>(lion?.AnimalCategory);
    }
}