namespace NoorGeneralHospital.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ini : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Doctors", "DOB", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Doctors", "DOB", c => c.DateTime(nullable: false));
        }
    }
}
