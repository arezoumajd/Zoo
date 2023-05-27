using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ZooDomain.DataModels
{
    public class Zoo
    {
        public IEnumerable<Animal> Animals { get; set; }
    }
}