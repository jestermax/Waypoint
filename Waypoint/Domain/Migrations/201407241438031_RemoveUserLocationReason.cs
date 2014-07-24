namespace Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemoveUserLocationReason : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.UserLocations", "UserLocationReason_Id", "dbo.UserLocationReasons");
            DropIndex("dbo.UserLocations", new[] { "UserLocationReason_Id" });
            DropColumn("dbo.UserLocations", "UserLocationReason_Id");
            DropTable("dbo.UserLocationReasons");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.UserLocationReasons",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.UserLocations", "UserLocationReason_Id", c => c.String(nullable: false, maxLength: 128));
            CreateIndex("dbo.UserLocations", "UserLocationReason_Id");
            AddForeignKey("dbo.UserLocations", "UserLocationReason_Id", "dbo.UserLocationReasons", "Id", cascadeDelete: true);
        }
    }
}
