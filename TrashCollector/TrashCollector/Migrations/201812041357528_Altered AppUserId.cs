namespace TrashCollector.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AlteredAppUserId : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.Customers", name: "UserID", newName: "AppUserID");
            RenameIndex(table: "dbo.Customers", name: "IX_UserID", newName: "IX_AppUserID");
        }
        
        public override void Down()
        {
            RenameIndex(table: "dbo.Customers", name: "IX_AppUserID", newName: "IX_UserID");
            RenameColumn(table: "dbo.Customers", name: "AppUserID", newName: "UserID");
        }
    }
}
