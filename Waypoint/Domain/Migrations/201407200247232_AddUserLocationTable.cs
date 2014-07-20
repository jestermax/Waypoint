namespace Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddUserLocationTable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.UserLocations",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Location = c.Geography(nullable: false),
                        Address = c.String(nullable: false, maxLength: 256),
                        Accuracy = c.Double(nullable: false),
                        Speed = c.Double(nullable: false),
                        DateSent = c.DateTime(nullable: false),
                        DateReceived = c.DateTime(nullable: false),
                        User_Id = c.String(nullable: false, maxLength: 128),
                        UserLocationReason_Id = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.User_Id, cascadeDelete: true)
                .ForeignKey("dbo.UserLocationReasons", t => t.UserLocationReason_Id, cascadeDelete: true)
                .Index(t => t.User_Id)
                .Index(t => t.UserLocationReason_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.UserLocations", "UserLocationReason_Id", "dbo.UserLocationReasons");
            DropForeignKey("dbo.UserLocations", "User_Id", "dbo.AspNetUsers");
            DropIndex("dbo.UserLocations", new[] { "UserLocationReason_Id" });
            DropIndex("dbo.UserLocations", new[] { "User_Id" });
            DropTable("dbo.UserLocations");
        }
    }
}
