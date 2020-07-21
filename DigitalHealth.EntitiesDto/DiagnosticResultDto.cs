using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DigitalHealth.Web.EntitiesDto
{
    public class DiagnosticResultDto
    {
        public DiseaseDto Disease { get; set; }  
        public int NumberOfCoincidences { get; set; }
    }
}