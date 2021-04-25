namespace NoorGeneralHospital.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updatecolumnspeacialities : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Specialities", "CreatedById", c => c.String());
            AddColumn("dbo.Specialities", "UpdatedById", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Specialities", "UpdatedById");
            DropColumn("dbo.Specialities", "CreatedById");
        }
    }
}
