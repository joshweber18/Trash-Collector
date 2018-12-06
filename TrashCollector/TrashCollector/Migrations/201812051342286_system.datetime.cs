namespace TrashCollector.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class systemdatetime : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Pickups", "VacaStart", c => c.DateTime());
            DropColumn("dbo.Pickups", "VacaDate");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Pickups", "VacaDate", c => c.DateTime());
            DropColumn("dbo.Pickups", "VacaStart");
        }
    }
}
