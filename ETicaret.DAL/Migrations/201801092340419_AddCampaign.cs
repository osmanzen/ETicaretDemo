namespace ETicaret.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddCampaign : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Campaign",
                c => new
                    {
                        CampaignID = c.Guid(nullable: false),
                        Title = c.String(nullable: false, maxLength: 50),
                        StartedDate = c.DateTime(nullable: false),
                        EndingDate = c.DateTime(nullable: false),
                        Discount = c.Decimal(nullable: false, precision: 18, scale: 2),
                        PictureUrl = c.String(nullable: false),
                        Status = c.Boolean(nullable: false),
                        IsActive = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.CampaignID);
            
            CreateTable(
                "dbo.ProductCampaign",
                c => new
                    {
                        CampaignID = c.Guid(nullable: false),
                        ProductID = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => new { t.CampaignID, t.ProductID })
                .ForeignKey("dbo.Campaign", t => t.CampaignID, cascadeDelete: true)
                .ForeignKey("dbo.Product", t => t.ProductID, cascadeDelete: true)
                .Index(t => t.CampaignID)
                .Index(t => t.ProductID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ProductCampaign", "ProductID", "dbo.Product");
            DropForeignKey("dbo.ProductCampaign", "CampaignID", "dbo.Campaign");
            DropIndex("dbo.ProductCampaign", new[] { "ProductID" });
            DropIndex("dbo.ProductCampaign", new[] { "CampaignID" });
            DropTable("dbo.ProductCampaign");
            DropTable("dbo.Campaign");
        }
    }
}
