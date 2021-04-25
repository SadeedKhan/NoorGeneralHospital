namespace NoorGeneralHospital.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Add_speciality : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Specialities",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        SpecialityName = c.String(),
                        IsActive = c.Boolean(),
                        CreatedOn = c.DateTime(),
                        UpdatedOn = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Specialities");
        }
    }
}
