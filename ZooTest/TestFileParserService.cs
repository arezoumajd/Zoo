using Xunit;
using Moq;
using ZooDomain.Services;
using ZooDomain.DTO;

public class ParseFileServiceTests
{
    [Fact]
    public void ParseAllFiles_ShouldReturnTrueOnSuccessfulParsing()
    {
        // Arrange
        var parseFileServiceMock = new Mock<IParseFileService>();
        var paths = new FilePaths
        {
            PricesFilePath = "./ZooCore/Files/prices.txt",
            AnimalsFilePath = "./ZooCore/Files/animals.csv",
            ZooFilePath = "./ZooCore/Files/zoo.xml"
        };

        // parseFileServiceMock.Setup(pfs => pfs.ParsePricesFile(paths.PricesFilePath));
        // parseFileServiceMock.Setup(pfs => pfs.ParseAnimalFile(paths.AnimalsFilePath));
        // parseFileServiceMock.Setup(pfs => pfs.ParseZooFile(paths.ZooFilePath, It.IsAny<List<AnimalCategory>>()));

        var parseFileService = parseFileServiceMock.Object;

        // Act
        bool result = parseFileService.ParseAllFiles(paths);

        // Assert
        Assert.True(result);

        // // Verify that the expected methods were called
        // parseFileServiceMock.Verify(pfs => pfs.ParsePricesFile(paths.PricesFilePath), Times.Once);
        // parseFileServiceMock.Verify(pfs => pfs.ParseAnimalFile(paths.AnimalsFilePath), Times.Once);
        // parseFileServiceMock.Verify(pfs => pfs.ParseZooFile(paths.ZooFilePath, It.IsAny<List<AnimalCategory>>()), Times.Once);
    }

    [Fact]
    public void ParseAllFiles_ShouldReturnFalseOnError()
    {
        // Arrange
        var parseFileServiceMock = new Mock<IParseFileService>();
        var paths = new FilePaths
        {
            PricesFilePath = "./ZooCore/Files/prices.txt",
            AnimalsFilePath = "./ZooCore/Files/animals.csv",
            ZooFilePath = "./ZooCore/Files/zoo.xml"
        };

        // parseFileServiceMock.Setup(pfs => pfs.ParsePricesFile(paths.PricesFilePath)).Throws(new Exception());
        // parseFileServiceMock.Setup(pfs => pfs.ParseAnimalFile(paths.AnimalsFilePath));
        // parseFileServiceMock.Setup(pfs => pfs.ParseZooFile(paths.ZooFilePath, It.IsAny<List<AnimalCategory>>()));

        var parseFileService = parseFileServiceMock.Object;

        // Act
        bool result = parseFileService.ParseAllFiles(paths);

        // Assert
        Assert.False(result);

        // Verify that the expected methods were called
        // parseFileServiceMock.Verify(pfs => pfs.ParsePricesFile(paths.PricesFilePath), Times.Once);
        // parseFileServiceMock.Verify(pfs => pfs.ParseAnimalFile(paths.AnimalsFilePath), Times.Never);
        // parseFileServiceMock.Verify(pfs => pfs.ParseZooFile(paths.ZooFilePath, It.IsAny<List<AnimalCategory>>()), Times.Never);
    }
}
