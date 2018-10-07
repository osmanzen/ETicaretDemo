namespace ETicaret.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddOrderStatus : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.OrderStatus",
                c => new
                    {
                        OrderStatusID = c.Guid(nullable: false),
                        Status = c.String(nullable: false, maxLength: 50),
                    })
                .PrimaryKey(t => t.OrderStatusID);
            
            AddColumn("dbo.Order", "OrderStatusID", c => c.Guid(nullable: false));
            CreateIndex("dbo.Order", "OrderStatusID");
            AddForeignKey("dbo.Order", "OrderStatusID", "dbo.OrderStatus", "OrderStatusID", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Order", "OrderStatusID", "dbo.OrderStatus");
            DropIndex("dbo.Order", new[] { "OrderStatusID" });
            DropColumn("dbo.Order", "OrderStatusID");
            DropTable("dbo.OrderStatus");
        }
    }
}
