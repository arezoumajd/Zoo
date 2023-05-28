using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ZooDomain.DataModels;

namespace ZooDomain.DTO
{
    public class CalculateDto
    {
        public Dictionary<string, decimal> FoodPrices { get; set; }
        public Zoo ZooDetails { get; set; }

        public CalculateDto()
        {
            FoodPrices = new Dictionary<string, decimal>();
            ZooDetails = new Zoo();
        }
    }
}