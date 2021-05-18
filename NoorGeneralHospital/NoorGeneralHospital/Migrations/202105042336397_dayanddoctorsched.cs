namespace NoorGeneralHospital.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class dayanddoctorsched : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.DoctorSchedules", "Description", c => c.String());
            AddColumn("dbo.DoctorSchedules", "CreatedOn", c => c.DateTime());
            AddColumn("dbo.DoctorSchedules", "CreatedById", c => c.String());
            AddColumn("dbo.DoctorSchedules", "UpdatedById", c => c.String());
            AddColumn("dbo.DoctorSchedules", "UpdatedOn", c => c.DateTime());
            DropColumn("dbo.DoctorSchedules", "MyProperty");
        }
        
        public override void Down()
        {
            AddColumn("dbo.DoctorSchedules", "MyProperty", c => c.Time(precision: 7));
            DropColumn("dbo.DoctorSchedules", "UpdatedOn");
            DropColumn("dbo.DoctorSchedules", "UpdatedById");
            DropColumn("dbo.DoctorSchedules", "CreatedById");
            DropColumn("dbo.DoctorSchedules", "CreatedOn");
            DropColumn("dbo.DoctorSchedules", "Description");
        }
    }
}
