namespace Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ExtendUserWithNameAndProfileImage : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.AspNetUsers", "TimeZone_Id", "dbo.TimeZones");
            DropIndex("dbo.AspNetUsers", new[] { "TimeZone_Id" });
            AddColumn("dbo.AspNetUsers", "FirstName", c => c.String(nullable: false, maxLength: 128));
            AddColumn("dbo.AspNetUsers", "LastName", c => c.String(nullable: false, maxLength: 128));
            AddColumn("dbo.AspNetUsers", "ProfileImage", c => c.String(nullable: false, maxLength: 128));
            AlterColumn("dbo.AspNetUsers", "TimeZone_Id", c => c.String(nullable: false, maxLength: 128));
            CreateIndex("dbo.AspNetUsers", "TimeZone_Id");
            AddForeignKey("dbo.AspNetUsers", "TimeZone_Id", "dbo.TimeZones", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUsers", "TimeZone_Id", "dbo.TimeZones");
            DropIndex("dbo.AspNetUsers", new[] { "TimeZone_Id" });
            AlterColumn("dbo.AspNetUsers", "TimeZone_Id", c => c.String(maxLength: 128));
            DropColumn("dbo.AspNetUsers", "ProfileImage");
            DropColumn("dbo.AspNetUsers", "LastName");
            DropColumn("dbo.AspNetUsers", "FirstName");
            CreateIndex("dbo.AspNetUsers", "TimeZone_Id");
            AddForeignKey("dbo.AspNetUsers", "TimeZone_Id", "dbo.TimeZones", "Id");
        }
    }
}
