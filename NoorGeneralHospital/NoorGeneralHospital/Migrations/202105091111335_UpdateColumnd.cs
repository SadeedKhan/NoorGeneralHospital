namespace NoorGeneralHospital.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateColumnd : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.DoctorSchedules", "StartTime", c => c.Time(precision: 7));
            AlterColumn("dbo.DoctorSchedules", "EndTime", c => c.Time(precision: 7));
            DropColumn("dbo.DoctorSchedules", "AvailableDate");
        }
        
        public override void Down()
        {
            AddColumn("dbo.DoctorSchedules", "AvailableDate", c => c.DateTime(nullable: false));
            AlterColumn("dbo.DoctorSchedules", "EndTime", c => c.String());
            AlterColumn("dbo.DoctorSchedules", "StartTime", c => c.String());
        }
    }
}
