namespace DigitalHealth.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class update_2 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Users", "ProfileId", c => c.Guid());
            CreateIndex("dbo.Users", "ProfileId");
            AddForeignKey("dbo.Users", "ProfileId", "dbo.Profiles", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Users", "ProfileId", "dbo.Profiles");
            DropIndex("dbo.Users", new[] { "ProfileId" });
            DropColumn("dbo.Users", "ProfileId");
        }
    }
}
