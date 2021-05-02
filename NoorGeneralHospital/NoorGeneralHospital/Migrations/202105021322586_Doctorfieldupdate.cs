namespace NoorGeneralHospital.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Doctorfieldupdate : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Doctors", "Email", c => c.String());
            AddColumn("dbo.Doctors", "Phone", c => c.String());
            AddColumn("dbo.Doctors", "Gender", c => c.Int(nullable: false));
            AddColumn("dbo.Doctors", "LocationId", c => c.Int(nullable: false));
            AddColumn("dbo.Doctors", "DOB", c => c.DateTime(nullable: false));
            AddColumn("dbo.Doctors", "Address", c => c.String());
            AddColumn("dbo.Doctors", "ShortBioGraphy", c => c.String());
            DropColumn("dbo.Doctors", "Description");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Doctors", "Description", c => c.String());
            DropColumn("dbo.Doctors", "ShortBioGraphy");
            DropColumn("dbo.Doctors", "Address");
            DropColumn("dbo.Doctors", "DOB");
            DropColumn("dbo.Doctors", "LocationId");
            DropColumn("dbo.Doctors", "Gender");
            DropColumn("dbo.Doctors", "Phone");
            DropColumn("dbo.Doctors", "Email");
        }
    }
}
