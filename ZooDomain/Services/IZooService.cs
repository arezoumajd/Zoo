using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ZooDomain.DataModels;

namespace ZooDomain.Services
{
    public interface IZooService
    {
        // public decimal CalculateTotalCost(string priceFilePath, string animalFilePath, string zooFilePath);
        public decimal CalculateTotalCost();
    }
}