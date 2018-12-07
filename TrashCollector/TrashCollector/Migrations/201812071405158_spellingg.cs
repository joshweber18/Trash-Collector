namespace TrashCollector.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class spellingg : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Customers", "BalanceDue", c => c.Double(nullable: false));
            DropColumn("dbo.Customers", "BalanaceDue");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Customers", "BalanaceDue", c => c.Double(nullable: false));
            DropColumn("dbo.Customers", "BalanceDue");
        }
    }
}
