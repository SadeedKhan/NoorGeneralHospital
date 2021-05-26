namespace NoorGeneralHospital.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class doctorschs : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.DoctorSchedules", "StartTime", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.DoctorSchedules", "StartTime", c => c.DateTime());
        }
    }
}
