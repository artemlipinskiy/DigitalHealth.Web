using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DigitalHealth.Web.EntitiesDto
{
    public class MarkDto
    {
        public Guid Id { get; set; }
        public int Value { get; set; }
        public string Comment { get; set; }
        public DateTime CreateDate { get; set; }

        public string Login { get; set; }
        public Guid UserId { get; set; }
        public string MethodName { get; set; }
        public Guid MethodOfTreatmentId { get; set; }
    }
}