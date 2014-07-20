namespace Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddApiTokenTable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ApiTokens",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Token = c.String(nullable: false, maxLength: 128),
                        DateIssued = c.DateTime(nullable: false),
                        DateExpires = c.DateTime(nullable: false),
                        User_Id = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.User_Id, cascadeDelete: true)
                .Index(t => t.User_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ApiTokens", "User_Id", "dbo.AspNetUsers");
            DropIndex("dbo.ApiTokens", new[] { "User_Id" });
            DropTable("dbo.ApiTokens");
        }
    }
}
