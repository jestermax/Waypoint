namespace Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddAccountToApplicationUser : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Accounts",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false, maxLength: 128),
                        DateCreated = c.DateTime(nullable: false),
                        DateModified = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.AspNetUsers", "Account_Id", c => c.String(maxLength: 128));
            CreateIndex("dbo.AspNetUsers", "Account_Id");
            AddForeignKey("dbo.AspNetUsers", "Account_Id", "dbo.Accounts", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUsers", "Account_Id", "dbo.Accounts");
            DropIndex("dbo.AspNetUsers", new[] { "Account_Id" });
            DropColumn("dbo.AspNetUsers", "Account_Id");
            DropTable("dbo.Accounts");
        }
    }
}
