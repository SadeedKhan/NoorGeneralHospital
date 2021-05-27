namespace NoorGeneralHospital.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addloc : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Locations", "NormalizeLocationName", c => c.String());
        }
        
        public override void Down()
        {
            AddColumn("dbo.Locations", "NLocationName", c => c.String());
            DropColumn("dbo.Locations", "NormalizeLocationName");
        }
    }
}
