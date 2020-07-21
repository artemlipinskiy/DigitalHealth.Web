using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DigitalHealth.Web.Entities
{
    public class Mark
    {
        public Guid Id { get; set; }
        public int Value { get; set; }
        public string Comment { get; set; }
        public DateTime CreateDate { get; set; }

        public User User { get; set; }
        public Guid UserId { get; set; }
        public MethodOfTreatment MethodOfTreatment { get; set; }
        public Guid MethodOfTreatmentId { get; set; }
    }
}