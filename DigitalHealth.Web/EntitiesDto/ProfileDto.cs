﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DigitalHealth.Web.EntitiesDto
{
    public class ProfileDto
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MiddleName { get; set; }
        public string Gender { get; set; }
    }
}