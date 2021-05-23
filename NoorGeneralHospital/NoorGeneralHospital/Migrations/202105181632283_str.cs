namespace NoorGeneralHospital.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class str : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.DoctorSchedules", "StartTime", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.DoctorSchedules", "StartTime", c => c.String());
        }
    }
}
