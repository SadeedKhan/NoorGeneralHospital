namespace NoorGeneralHospital.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class gender : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Genders",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.Doctors", "GenderId", c => c.Int(nullable: false));
            DropColumn("dbo.Doctors", "Gender");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Doctors", "Gender", c => c.Int(nullable: false));
            DropColumn("dbo.Doctors", "GenderId");
            DropTable("dbo.Genders");
        }
    }
}
