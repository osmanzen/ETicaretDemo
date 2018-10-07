namespace ETicaret.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class deleteCampaign : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.CampaignProducts", "Campaign_CampaignID", "dbo.Campaigns");
            DropForeignKey("dbo.CampaignProducts", "Product_ProductID", "dbo.Product");
            DropIndex("dbo.CampaignProducts", new[] { "Campaign_CampaignID" });
            DropIndex("dbo.CampaignProducts", new[] { "Product_ProductID" });
            DropTable("dbo.Campaigns");
            DropTable("dbo.CampaignProducts");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.CampaignProducts",
                c => new
                    {
                        Campaign_CampaignID = c.Guid(nullable: false),
                        Product_ProductID = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => new { t.Campaign_CampaignID, t.Product_ProductID });
            
            CreateTable(
                "dbo.Campaigns",
                c => new
                    {
                        CampaignID = c.Guid(nullable: false),
                        Title = c.String(),
                        StartedDate = c.DateTime(nullable: false),
                        EndingDate = c.DateTime(nullable: false),
                        Discount = c.Decimal(nullable: false, precision: 18, scale: 2),
                        PictureUrl = c.String(),
                        Status = c.Boolean(nullable: false),
                        IsActive = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.CampaignID);
            
            CreateIndex("dbo.CampaignProducts", "Product_ProductID");
            CreateIndex("dbo.CampaignProducts", "Campaign_CampaignID");
            AddForeignKey("dbo.CampaignProducts", "Product_ProductID", "dbo.Product", "ProductID", cascadeDelete: true);
            AddForeignKey("dbo.CampaignProducts", "Campaign_CampaignID", "dbo.Campaigns", "CampaignID", cascadeDelete: true);
        }
    }
}
