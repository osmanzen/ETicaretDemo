namespace ETicaret.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UserCardOptional : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Order", "UserCardID", "dbo.UserCard");
            DropIndex("dbo.Order", new[] { "UserCardID" });
            AlterColumn("dbo.Order", "UserCardID", c => c.Guid());
            CreateIndex("dbo.Order", "UserCardID");
            AddForeignKey("dbo.Order", "UserCardID", "dbo.UserCard", "UserCardID");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Order", "UserCardID", "dbo.UserCard");
            DropIndex("dbo.Order", new[] { "UserCardID" });
            AlterColumn("dbo.Order", "UserCardID", c => c.Guid(nullable: false));
            CreateIndex("dbo.Order", "UserCardID");
            AddForeignKey("dbo.Order", "UserCardID", "dbo.UserCard", "UserCardID", cascadeDelete: true);
        }
    }
}
