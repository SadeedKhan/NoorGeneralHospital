namespace NoorGeneralHospital.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initw : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Specialities", "NormalizeSpecialityName", c => c.String());
        }
        
        public override void Down()
        {
            AddColumn("dbo.Specialities", "NSpecialityName", c => c.String());
            DropColumn("dbo.Specialities", "NormalizeSpecialityName");
        }
    }
}
