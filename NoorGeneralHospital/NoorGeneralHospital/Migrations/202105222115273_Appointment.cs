namespace NoorGeneralHospital.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Appointment : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Appointments",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        PatientAge = c.Int(nullable: false),
                        PatientName = c.String(),
                        PatientEmail = c.String(),
                        PatientPhone = c.String(),
                        SpecialityId = c.Int(nullable: false),
                        DoctorId = c.Int(nullable: false),
                        AppointmentDate = c.String(),
                        Description = c.String(),
                        Reason = c.String(),
                        IsActive = c.Boolean(nullable: false),
                        StatusId = c.Int(nullable: false),
                        CreatedOn = c.DateTime(),
                        CreatedById = c.String(),
                        UpdatedById = c.String(),
                        UpdatedOn = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Appointments");
        }
    }
}
