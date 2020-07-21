using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DigitalHealth.Web.Entities
{
    public class MethodOfTreatment
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Source { get; set; }
        public Guid DiseaseId { get; set; }
        public Disease Disease { get; set; }
    }
}