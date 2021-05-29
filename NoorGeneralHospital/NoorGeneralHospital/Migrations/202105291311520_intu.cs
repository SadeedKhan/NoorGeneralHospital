namespace NoorGeneralHospital.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class intu : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Activities", "AppointmentDate", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Activities", "AppointmentDate", c => c.DateTime(nullable: false));
        }
    }
}
