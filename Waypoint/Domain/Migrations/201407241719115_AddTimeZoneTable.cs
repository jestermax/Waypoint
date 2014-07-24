namespace Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddTimeZoneTable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.TimeZones",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false, maxLength: 128),
                        Offset = c.Int(nullable: false),
                        SortOrder = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.AspNetUsers", "TimeZone_Id", c => c.String(maxLength: 128));
            CreateIndex("dbo.AspNetUsers", "TimeZone_Id");
            AddForeignKey("dbo.AspNetUsers", "TimeZone_Id", "dbo.TimeZones", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUsers", "TimeZone_Id", "dbo.TimeZones");
            DropIndex("dbo.AspNetUsers", new[] { "TimeZone_Id" });
            DropColumn("dbo.AspNetUsers", "TimeZone_Id");
            DropTable("dbo.TimeZones");
        }
    }
}
