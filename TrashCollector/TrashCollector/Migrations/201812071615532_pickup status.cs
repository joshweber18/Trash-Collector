namespace TrashCollector.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class pickupstatus : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Customers", "PickupStatus", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Customers", "PickupStatus");
        }
    }
}
