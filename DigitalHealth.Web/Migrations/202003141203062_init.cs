namespace DigitalHealth.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class init : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Diseases",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Name = c.String(),
                        Description = c.String(),
                        CIDID = c.Guid(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Symptoms",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Name = c.String(),
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.ICDs",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Code = c.String(),
                        Name = c.String(),
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Marks",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Value = c.Int(nullable: false),
                        Comment = c.String(),
                        CreateDate = c.DateTime(nullable: false),
                        UserId = c.Guid(nullable: false),
                        MethodOfTreatmentId = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.MethodOfTreatments", t => t.MethodOfTreatmentId, cascadeDelete: true)
                .ForeignKey("dbo.Users", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.MethodOfTreatmentId);
            
            CreateTable(
                "dbo.MethodOfTreatments",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Title = c.String(),
                        Description = c.String(),
                        Source = c.String(),
                        DiseaseId = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Login = c.String(),
                        HashPassword = c.String(),
                        RoleId = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Roles", t => t.RoleId, cascadeDelete: true)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.Roles",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Name = c.String(),
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Profiles",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        FirstName = c.String(),
                        LastName = c.String(),
                        MiddlleName = c.String(),
                        Gender = c.String(),
                        UserId = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.SymptomDiseases",
                c => new
                    {
                        Symptom_Id = c.Guid(nullable: false),
                        Disease_Id = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => new { t.Symptom_Id, t.Disease_Id })
                .ForeignKey("dbo.Symptoms", t => t.Symptom_Id, cascadeDelete: true)
                .ForeignKey("dbo.Diseases", t => t.Disease_Id, cascadeDelete: true)
                .Index(t => t.Symptom_Id)
                .Index(t => t.Disease_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Marks", "UserId", "dbo.Users");
            DropForeignKey("dbo.Users", "RoleId", "dbo.Roles");
            DropForeignKey("dbo.Marks", "MethodOfTreatmentId", "dbo.MethodOfTreatments");
            DropForeignKey("dbo.SymptomDiseases", "Disease_Id", "dbo.Diseases");
            DropForeignKey("dbo.SymptomDiseases", "Symptom_Id", "dbo.Symptoms");
            DropIndex("dbo.SymptomDiseases", new[] { "Disease_Id" });
            DropIndex("dbo.SymptomDiseases", new[] { "Symptom_Id" });
            DropIndex("dbo.Users", new[] { "RoleId" });
            DropIndex("dbo.Marks", new[] { "MethodOfTreatmentId" });
            DropIndex("dbo.Marks", new[] { "UserId" });
            DropTable("dbo.SymptomDiseases");
            DropTable("dbo.Profiles");
            DropTable("dbo.Roles");
            DropTable("dbo.Users");
            DropTable("dbo.MethodOfTreatments");
            DropTable("dbo.Marks");
            DropTable("dbo.ICDs");
            DropTable("dbo.Symptoms");
            DropTable("dbo.Diseases");
        }
    }
}
