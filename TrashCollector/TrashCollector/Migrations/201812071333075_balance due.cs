namespace TrashCollector.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class balancedue : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Customers", "BalanaceDue", c => c.Double(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Customers", "BalanaceDue");
        }
    }
}
