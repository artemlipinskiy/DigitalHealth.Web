namespace DigitalHealth.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class update : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Diseases", "ICDID", c => c.Guid());
            CreateIndex("dbo.Diseases", "ICDID");
            CreateIndex("dbo.MethodOfTreatments", "DiseaseId");
            AddForeignKey("dbo.Diseases", "ICDID", "dbo.ICDs", "Id");
            AddForeignKey("dbo.MethodOfTreatments", "DiseaseId", "dbo.Diseases", "Id", cascadeDelete: true);
            DropColumn("dbo.Diseases", "CIDID");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Diseases", "CIDID", c => c.Guid());
            DropForeignKey("dbo.MethodOfTreatments", "DiseaseId", "dbo.Diseases");
            DropForeignKey("dbo.Diseases", "ICDID", "dbo.ICDs");
            DropIndex("dbo.MethodOfTreatments", new[] { "DiseaseId" });
            DropIndex("dbo.Diseases", new[] { "ICDID" });
            DropColumn("dbo.Diseases", "ICDID");
        }
    }
}
