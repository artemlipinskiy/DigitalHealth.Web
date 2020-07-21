using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using DigitalHealth.Web.Entities;

namespace DigitalHealth.Services
{
    public class DHContext: DbContext
    {
        public DHContext() : base("Dbconnection") { }
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Disease> Diseases { get; set; }
        public DbSet<Symptom> Symptoms { get; set; }
        //public DbSet<DiseasesSymptom> DiseasesSymptoms { get; set; }
        public DbSet<Mark> Marks { get; set; }
        public DbSet<Profile> Profiles { get; set; }
        public DbSet<MethodOfTreatment> MethodOfTreatments { get; set; }
        public DbSet<ICD> ICDs { get; set; }

    }
}