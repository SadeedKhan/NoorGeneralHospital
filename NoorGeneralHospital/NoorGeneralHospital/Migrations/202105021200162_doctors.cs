namespace NoorGeneralHospital.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class doctors : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Locations", "CreatedOn", c => c.DateTime());
            AddColumn("dbo.Locations", "CreatedById", c => c.String());
            AddColumn("dbo.Locations", "UpdatedById", c => c.String());
            AddColumn("dbo.Locations", "UpdatedOn", c => c.DateTime());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Locations", "UpdatedOn");
            DropColumn("dbo.Locations", "UpdatedById");
            DropColumn("dbo.Locations", "CreatedById");
            DropColumn("dbo.Locations", "CreatedOn");
        }
    }
}
