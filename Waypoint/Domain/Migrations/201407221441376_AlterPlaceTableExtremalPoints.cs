namespace Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AlterPlaceTableExtremalPoints : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Places", "MinimumLatitude", c => c.Double(nullable: false));
            AddColumn("dbo.Places", "MinimumLongitude", c => c.Double(nullable: false));
            AddColumn("dbo.Places", "MaximumLatitude", c => c.Double(nullable: false));
            AddColumn("dbo.Places", "MaximumLongitude", c => c.Double(nullable: false));
            DropColumn("dbo.Places", "MinimumLocation");
            DropColumn("dbo.Places", "MaximumLocation");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Places", "MaximumLocation", c => c.Geography(nullable: false));
            AddColumn("dbo.Places", "MinimumLocation", c => c.Geography(nullable: false));
            DropColumn("dbo.Places", "MaximumLongitude");
            DropColumn("dbo.Places", "MaximumLatitude");
            DropColumn("dbo.Places", "MinimumLongitude");
            DropColumn("dbo.Places", "MinimumLatitude");
        }
    }
}
