using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DigitalHealth.Web.Entities
{
    public class Symptom
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public ICollection<Disease> Diseases { get; set; }
        public Symptom()
        {
            Diseases = new List<Disease>();
        }
    }
}