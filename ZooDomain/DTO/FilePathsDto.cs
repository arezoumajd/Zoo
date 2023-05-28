using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ZooDomain.DTO
{
    public class FilePathsDto
    {
        public string PricesFilePath { get; set; }
        public string AnimalsFilePath { get; set; }
        public string ZooFilePath { get; set; }

        public FilePathsDto()
        {
            PricesFilePath = string.Empty;
            AnimalsFilePath = string.Empty;
            ZooFilePath = string.Empty;
        }
    }
}