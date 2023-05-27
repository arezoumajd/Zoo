using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ZooDomain.DataModels;

namespace ZooDomain.Services
{
    public interface IParseFileService
    {
        Dictionary<string, decimal> ParsePricesFile(string path);
        List<AnimalCategory> ParseAnimalFile(string path);
        Zoo ParseZooFile(string path, List<AnimalCategory> animalCategories);
    }
}