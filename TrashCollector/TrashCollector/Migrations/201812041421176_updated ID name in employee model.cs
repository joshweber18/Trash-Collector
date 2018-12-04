namespace TrashCollector.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updatedIDnameinemployeemodel : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.Employees", name: "UserID", newName: "AppUserID");
            RenameIndex(table: "dbo.Employees", name: "IX_UserID", newName: "IX_AppUserID");
        }
        
        public override void Down()
        {
            RenameIndex(table: "dbo.Employees", name: "IX_AppUserID", newName: "IX_UserID");
            RenameColumn(table: "dbo.Employees", name: "AppUserID", newName: "UserID");
        }
    }
}
