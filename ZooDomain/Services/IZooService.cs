using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ZooDomain.DataModels;
using ZooDomain.DTO;

namespace ZooDomain.Services
{
    public interface IZooService
    {
        public decimal CalculateTotalCost(CalculateDto dto);
    }
}