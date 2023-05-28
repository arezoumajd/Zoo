using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ZooDomain.DataModels
{
    public class Animal
    {
        public string Name { get; set; }
        public AnimalCategory AnimalCategory { get; set; }
        public decimal Weight { get; set; }

        public Animal()
        {
            Name = string.Empty;
            AnimalCategory = new AnimalCategory();
        }
    }
}