namespace NoorGeneralHospital.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class doctorschss : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.DoctorSchedules", "EndTime", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.DoctorSchedules", "EndTime", c => c.DateTime());
        }
    }
}
