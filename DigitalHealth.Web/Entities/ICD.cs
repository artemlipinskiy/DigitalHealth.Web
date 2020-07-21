using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DigitalHealth.Web.Entities
{
    public class ICD
    {
        public Guid Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }
}