using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ZooDomain.Enums;

namespace ZooDomain.DataModels
{
    public class AnimalCategory
    {
        public string Name { get; set; }
        public AnimalTypeEnum Type { get; set; }
        public decimal RatePerKg { get; set; }
        public decimal MeatPercentage { get; set; }
    }
}