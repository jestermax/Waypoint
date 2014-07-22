namespace Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    using System.Data.Entity.Spatial;
    
    public partial class ExtendPlaceWithMinMaxLocations : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Places", "MinimumLocation", c => c.Geography(nullable: false));
            AddColumn("dbo.Places", "MaximumLocation", c => c.Geography(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Places", "MaximumLocation");
            DropColumn("dbo.Places", "MinimumLocation");
        }
    }
}
