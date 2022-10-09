namespace NoorGeneralHospital.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Opp : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Appointments", "UserId", c => c.String(nullable: false, maxLength: 128));
            AlterColumn("dbo.Appointments", "AppointmentDate", c => c.DateTime(nullable: false));
            CreateIndex("dbo.Appointments", "UserId");
            AddForeignKey("dbo.Appointments", "UserId", "dbo.AspNetUsers", "Id", cascadeDelete: true);
            DropColumn("dbo.Appointments", "PatientAge");
            DropColumn("dbo.Appointments", "PatientName");
            DropColumn("dbo.Appointments", "PatientEmail");
            DropColumn("dbo.Appointments", "PatientPhone");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Appointments", "PatientPhone", c => c.String());
            AddColumn("dbo.Appointments", "PatientEmail", c => c.String());
            AddColumn("dbo.Appointments", "PatientName", c => c.String());
            AddColumn("dbo.Appointments", "PatientAge", c => c.Int(nullable: false));
            DropForeignKey("dbo.Appointments", "UserId", "dbo.AspNetUsers");
            DropIndex("dbo.Appointments", new[] { "UserId" });
            AlterColumn("dbo.Appointments", "AppointmentDate", c => c.String());
            DropColumn("dbo.Appointments", "UserId");
        }
    }
}
