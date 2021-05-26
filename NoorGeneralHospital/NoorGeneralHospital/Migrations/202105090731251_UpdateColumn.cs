namespace NoorGeneralHospital.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateColumn : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.DoctorSchedules", "AvailableDate", c => c.DateTime(nullable: false));
            AlterColumn("dbo.DoctorSchedules", "StartTime", c => c.String());
            AlterColumn("dbo.DoctorSchedules", "EndTime", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.DoctorSchedules", "EndTime", c => c.Time(precision: 7));
            AlterColumn("dbo.DoctorSchedules", "StartTime", c => c.Time(precision: 7));
            DropColumn("dbo.DoctorSchedules", "AvailableDate");
        }
    }
}
