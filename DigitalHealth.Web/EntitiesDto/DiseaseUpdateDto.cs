using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DigitalHealth.Web.EntitiesDto
{
    public class DiseaseUpdateDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public Guid? ICDID { get; set; }
        public List<Guid> SymptomIds { get; set; }
        public List<string> SymptomNames { get; set; }
    }
}