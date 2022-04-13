namespace NoorGeneralHospital.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addservices : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Services",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ServiceTitle = c.String(),
                        ImagePath = c.String(),
                        Description = c.String(),
                        IsActive = c.Boolean(nullable: false),
                        CreatedOn = c.DateTime(),
                        CreatedById = c.String(),
                        UpdatedById = c.String(),
                        UpdatedOn = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Doctor_GetDoctorDetailsByID",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserName = c.String(),
                        Email = c.String(),
                        Phone = c.String(),
                        Speciality = c.String(),
                        Gender = c.String(),
                        Location = c.String(),
                        Services = c.String(),
                        Education = c.String(),
                        Experience = c.String(),
                        ImagePath = c.String(),
                        DOB = c.String(),
                        Address = c.String(),
                        ShortBioGraphy = c.String(),
                        AvailableTime = c.String(),
                        AvailableDay = c.String(),
                        AvailableDays = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Doctor_GetDoctorDetailsByID");
            DropTable("dbo.Services");
        }
    }
}
