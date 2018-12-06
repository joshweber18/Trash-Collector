namespace TrashCollector.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class thing : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Pickups", "VacaDate", c => c.DateTime());
            DropColumn("dbo.Pickups", "VacaStart");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Pickups", "VacaStart", c => c.DateTime());
            DropColumn("dbo.Pickups", "VacaDate");
        }
    }
}
