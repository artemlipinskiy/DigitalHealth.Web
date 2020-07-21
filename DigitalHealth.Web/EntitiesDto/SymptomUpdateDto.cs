using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DigitalHealth.Web.EntitiesDto
{
    public class SymptomUpdateDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }
}