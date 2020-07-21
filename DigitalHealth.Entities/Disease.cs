using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DigitalHealth.Web.Entities
{
    public class Disease
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public Guid? ICDID { get; set; }
        public ICD ICD { get; set; }
        public ICollection<Symptom> Symptoms { get; set; }

        public Disease()
        {
            Symptoms = new List<Symptom>();
        }
    }
}