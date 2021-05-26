namespace NoorGeneralHospital.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class doctorsc : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.DoctorSchedules", "AvailableDay", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.DoctorSchedules", "AvailableDay");
        }
    }
}
