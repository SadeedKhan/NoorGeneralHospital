namespace NoorGeneralHospital.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updateidspecial : DbMigration
    {
        public override void Up()
        {
            DropPrimaryKey("dbo.Specialities");
            DropColumn("dbo.Specialities", "Id");
            AddColumn("dbo.Specialities", "Id", c => c.Int(nullable: false, identity: true));
            AddPrimaryKey("dbo.Specialities", "Id");
        }
        
        public override void Down()
        {
            DropPrimaryKey("dbo.Specialities");
            AlterColumn("dbo.Specialities", "Id", c => c.Guid(nullable: false));
            AddPrimaryKey("dbo.Specialities", "Id");
        }
    }
}
