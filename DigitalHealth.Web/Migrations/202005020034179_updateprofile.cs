namespace DigitalHealth.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updateprofile : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Profiles", "MiddleName", c => c.String());
            DropColumn("dbo.Profiles", "MiddlleName");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Profiles", "MiddlleName", c => c.String());
            DropColumn("dbo.Profiles", "MiddleName");
        }
    }
}
