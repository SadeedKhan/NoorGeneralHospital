namespace NoorGeneralHospital.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class doctorsch : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.DoctorSchedules", "StartTime", c => c.DateTime());
            AlterColumn("dbo.DoctorSchedules", "EndTime", c => c.DateTime());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.DoctorSchedules", "EndTime", c => c.Time(precision: 7));
            AlterColumn("dbo.DoctorSchedules", "StartTime", c => c.Time(precision: 7));
        }
    }
}
