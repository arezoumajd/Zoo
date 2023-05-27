using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Xml;
using ZooDomain.DataModels;

namespace ZooDomain.DTO
{
    public class AnimalXMLDto
    {
        public XmlNodeList XMLData { get; set; }
        public AnimalCategory AnimalCategory { get; set; }
    }
}