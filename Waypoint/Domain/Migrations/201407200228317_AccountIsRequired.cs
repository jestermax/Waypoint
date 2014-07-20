namespace Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AccountIsRequired : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.AspNetUsers", "Account_Id", "dbo.Accounts");
            DropIndex("dbo.AspNetUsers", new[] { "Account_Id" });
            AlterColumn("dbo.AspNetUsers", "Account_Id", c => c.String(nullable: false, maxLength: 128));
            CreateIndex("dbo.AspNetUsers", "Account_Id");
            AddForeignKey("dbo.AspNetUsers", "Account_Id", "dbo.Accounts", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUsers", "Account_Id", "dbo.Accounts");
            DropIndex("dbo.AspNetUsers", new[] { "Account_Id" });
            AlterColumn("dbo.AspNetUsers", "Account_Id", c => c.String(maxLength: 128));
            CreateIndex("dbo.AspNetUsers", "Account_Id");
            AddForeignKey("dbo.AspNetUsers", "Account_Id", "dbo.Accounts", "Id");
        }
    }
}
