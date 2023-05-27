using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
using ZooDomain.DataModels;
using ZooDomain.DTO;
using ZooDomain.Enums;
using ZooDomain.Services;

namespace ZooCore.Services
{
    public class ParseFileService : IParseFileService
    {
        public Dictionary<string, decimal> FoodPrices;
        public List<AnimalCategory> Categories;
        public Zoo ZooDetail;
        private void ParsePricesFile(string path)
        {
            string priceFileContent = File.ReadAllText(path);
            string[] lines = priceFileContent.Split('\n'); //animalFileContent.Split(Environment.NewLine);
            Dictionary<string, decimal> prices = new Dictionary<string, decimal>();
            foreach (string line in lines)
            {
                string[] parts = line.Split('=');
                string foodType = parts[0].Trim();
                decimal price = decimal.Parse(parts[1].Trim());
                prices.Add(foodType, price);
            }
            this.FoodPrices = prices;
        }

        private void ParseAnimalFile(string path)
        {
            string animalFileContent = File.ReadAllText(path);
            List<AnimalCategory> animalCategories = new List<AnimalCategory>();
            string[] lines = animalFileContent.Split(Environment.NewLine);
            foreach (string line in lines)
            {
                string[] parts = line.Split(';');
                var data = new AnimalCategory
                {
                    Name = parts[0].Trim(),
                    RatePerKg = decimal.Parse(parts[1]),
                };

                string dietType = parts[2].Trim();
                switch (dietType)
                {
                    case "meat":
                        data.Type = AnimalTypeEnum.Carnivores;
                        break;
                    case "fruit":
                        data.Type = AnimalTypeEnum.Herbivores;
                        break;
                    default:
                        data.Type = AnimalTypeEnum.Omnivores;
                        break;
                }

                if (parts.Length > 3 && data.Type == AnimalTypeEnum.Omnivores)
                {
                    var meatPercentage = parts[3].TrimEnd(new[] { '\r' });
                    data.MeatPercentage = decimal.Parse(meatPercentage.TrimEnd(new[] { '%' }));
                }

                if (data.Type == AnimalTypeEnum.Carnivores)
                {
                    data.MeatPercentage = 100;
                }

                animalCategories.Add(data);
            }

            this.Categories = animalCategories;
        }

        private void ParseZooFile(string path, List<AnimalCategory>? animalCategories)
        {
            Zoo zooDetails = new Zoo();
            List<Animal> animalList = new List<Animal>();
            XmlDocument doc = new XmlDocument();
            doc.Load(path);
            XmlElement? root = doc.DocumentElement;
            if (root != null)
            {
                List<AnimalXMLDto> nodes = new List<AnimalXMLDto>();
                foreach (var category in animalCategories)
                {
                    nodes.Add(new AnimalXMLDto
                    {
                        XMLData = root.GetElementsByTagName(category.Name),
                        AnimalCategory = category
                    });
                }

                foreach (var node in nodes)
                {
                    for (int i = 0; i < node.XMLData.Count; i++)
                    {
                        Animal animal = new Animal();
                        if (nodes[i] != null)
                        {
                            XmlAttribute nameAttribute = node.XMLData[i].Attributes["name"];
                            if (nameAttribute != null)
                            {
                                animal.Name = nameAttribute.Value;
                            }

                            XmlAttribute weightAttribute = node.XMLData[i].Attributes["kg"];
                            if (weightAttribute != null)
                            {
                                animal.Weight = decimal.Parse(weightAttribute.Value);
                            }
                        }
                        animal.AnimalCategory = node.AnimalCategory;
                        animalList.Add(animal);
                    }

                }
                zooDetails.Animals = animalList;
            }
            this.ZooDetail = zooDetails;
        }

        public bool ParseAllFiles(FilePaths paths)
        {
            try
            {
                if (paths.PricesFilePath != null)
                {
                    ParsePricesFile(paths.PricesFilePath);
                }

                if (paths.AnimalsFilePath != null)
                {
                    ParseAnimalFile(paths.AnimalsFilePath);
                }

                if (paths.ZooFilePath != null)
                {
                    ParseZooFile(paths.ZooFilePath, this.Categories);
                }

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public Dictionary<string, decimal> GetFoodPrices()
        {
            return this.FoodPrices;
        }

        public List<AnimalCategory> GetAnimalCategories()
        {
            return this.Categories;
        }

        public Zoo GetZoo()
        {
            return this.ZooDetail;
        }
    }
}