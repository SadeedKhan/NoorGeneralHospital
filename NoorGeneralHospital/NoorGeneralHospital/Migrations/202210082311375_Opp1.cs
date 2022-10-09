namespace NoorGeneralHospital.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Opp1 : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Doctor_GetDoctorDetailsByID", "AvailableTime");
            DropColumn("dbo.Doctor_GetDoctorDetailsByID", "AvailableDay");
            DropColumn("dbo.Doctor_GetDoctorDetailsByID", "AvailableDays");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Doctor_GetDoctorDetailsByID", "AvailableDays", c => c.String());
            AddColumn("dbo.Doctor_GetDoctorDetailsByID", "AvailableDay", c => c.String());
            AddColumn("dbo.Doctor_GetDoctorDetailsByID", "AvailableTime", c => c.String());
        }
    }
}
