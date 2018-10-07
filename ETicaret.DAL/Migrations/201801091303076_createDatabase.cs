namespace ETicaret.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class createDatabase : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.BasketProduct",
                c => new
                    {
                        UserID = c.Guid(nullable: false),
                        ProductID = c.Guid(nullable: false),
                        Count = c.Int(nullable: false),
                        IsActive = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => new { t.UserID, t.ProductID })
                .ForeignKey("dbo.Product", t => t.ProductID, cascadeDelete: true)
                .ForeignKey("dbo.User", t => t.UserID, cascadeDelete: true)
                .Index(t => t.UserID)
                .Index(t => t.ProductID);
            
            CreateTable(
                "dbo.Product",
                c => new
                    {
                        ProductID = c.Guid(nullable: false),
                        ModelID = c.Guid(),
                        ProductName = c.String(nullable: false, maxLength: 50),
                        UnitPrice = c.Decimal(nullable: false, precision: 18, scale: 2),
                        UnitsInStock = c.Int(nullable: false),
                        ViewCount = c.Int(nullable: false),
                        Description = c.String(nullable: false, maxLength: 250),
                        IsActive = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.ProductID)
                .ForeignKey("dbo.ProductModel", t => t.ModelID)
                .Index(t => t.ModelID);
            
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
            
            CreateTable(
                "dbo.OrderDetail",
                c => new
                    {
                        OrderID = c.Guid(nullable: false),
                        ProductID = c.Guid(nullable: false),
                        Count = c.Int(nullable: false),
                        IsActive = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => new { t.OrderID, t.ProductID })
                .ForeignKey("dbo.Order", t => t.OrderID, cascadeDelete: true)
                .ForeignKey("dbo.Product", t => t.ProductID, cascadeDelete: true)
                .Index(t => t.OrderID)
                .Index(t => t.ProductID);
            
            CreateTable(
                "dbo.Order",
                c => new
                    {
                        OrderID = c.Guid(nullable: false),
                        UserID = c.Guid(nullable: false),
                        UserDetailID = c.Guid(nullable: false),
                        AddressID = c.Guid(nullable: false),
                        PaymentTypeID = c.Guid(nullable: false),
                        UserCardID = c.Guid(nullable: false),
                        TotalAmount = c.Decimal(nullable: false, precision: 18, scale: 2),
                        CreatedTime = c.DateTime(nullable: false),
                        IsActive = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.OrderID)
                .ForeignKey("dbo.PaymentType", t => t.PaymentTypeID, cascadeDelete: true)
                .ForeignKey("dbo.User", t => t.UserID)
                .ForeignKey("dbo.UserAddress", t => t.AddressID)
                .ForeignKey("dbo.UserCard", t => t.UserCardID, cascadeDelete: true)
                .ForeignKey("dbo.UserDetail", t => t.UserDetailID)
                .Index(t => t.UserID)
                .Index(t => t.UserDetailID)
                .Index(t => t.AddressID)
                .Index(t => t.PaymentTypeID)
                .Index(t => t.UserCardID);
            
            CreateTable(
                "dbo.PaymentType",
                c => new
                    {
                        PaymentTypeID = c.Guid(nullable: false),
                        PaymentName = c.String(nullable: false, maxLength: 50),
                        IsActive = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.PaymentTypeID);
            
            CreateTable(
                "dbo.User",
                c => new
                    {
                        UserID = c.Guid(nullable: false),
                        UserTypeID = c.Guid(nullable: false),
                        FullName = c.String(nullable: false, maxLength: 50),
                        Email = c.String(nullable: false, maxLength: 50),
                        Password = c.String(nullable: false, maxLength: 50),
                        CreatedDate = c.DateTime(nullable: false),
                        IsActive = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.UserID)
                .ForeignKey("dbo.UserType", t => t.UserTypeID, cascadeDelete: true)
                .Index(t => t.UserTypeID);
            
            CreateTable(
                "dbo.ProductComment",
                c => new
                    {
                        ProductCommentID = c.Guid(nullable: false),
                        UserID = c.Guid(nullable: false),
                        ProductID = c.Guid(nullable: false),
                        Content = c.String(nullable: false, maxLength: 1000),
                        CreatedDate = c.DateTime(nullable: false),
                        IsActive = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.ProductCommentID)
                .ForeignKey("dbo.Product", t => t.ProductID, cascadeDelete: true)
                .ForeignKey("dbo.User", t => t.UserID, cascadeDelete: true)
                .Index(t => t.UserID)
                .Index(t => t.ProductID);
            
            CreateTable(
                "dbo.UserAddress",
                c => new
                    {
                        UserAddressID = c.Guid(nullable: false),
                        UserID = c.Guid(nullable: false),
                        DistrictID = c.Int(nullable: false),
                        AddressFullName = c.String(nullable: false, maxLength: 50),
                        Address = c.String(nullable: false, maxLength: 1000),
                        IsActive = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.UserAddressID)
                .ForeignKey("dbo.District", t => t.DistrictID, cascadeDelete: true)
                .ForeignKey("dbo.User", t => t.UserID, cascadeDelete: true)
                .Index(t => t.UserID)
                .Index(t => t.DistrictID);
            
            CreateTable(
                "dbo.District",
                c => new
                    {
                        DistrictID = c.Int(nullable: false, identity: true),
                        CityID = c.Int(nullable: false),
                        DistrictName = c.String(nullable: false, maxLength: 50),
                    })
                .PrimaryKey(t => t.DistrictID)
                .ForeignKey("dbo.City", t => t.CityID, cascadeDelete: true)
                .Index(t => t.CityID);
            
            CreateTable(
                "dbo.City",
                c => new
                    {
                        CityID = c.Int(nullable: false, identity: true),
                        CityName = c.String(nullable: false, maxLength: 50),
                    })
                .PrimaryKey(t => t.CityID);
            
            CreateTable(
                "dbo.UserCard",
                c => new
                    {
                        UserCardID = c.Guid(nullable: false),
                        UserID = c.Guid(nullable: false),
                        FullName = c.String(nullable: false, maxLength: 50),
                        CardNo = c.String(nullable: false, maxLength: 16),
                        SecurityCode = c.String(nullable: false, maxLength: 3),
                        ExpritionDate = c.DateTime(nullable: false),
                        IsActive = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.UserCardID)
                .ForeignKey("dbo.User", t => t.UserID, cascadeDelete: true)
                .Index(t => t.UserID);
            
            CreateTable(
                "dbo.UserDetail",
                c => new
                    {
                        UserDetailID = c.Guid(nullable: false),
                        UserID = c.Guid(nullable: false),
                        Gender = c.Boolean(nullable: false),
                        Telephone = c.String(nullable: false, maxLength: 11),
                        IsActive = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.UserDetailID)
                .ForeignKey("dbo.User", t => t.UserID, cascadeDelete: true)
                .Index(t => t.UserID);
            
            CreateTable(
                "dbo.UserType",
                c => new
                    {
                        UserTypeID = c.Guid(nullable: false),
                        UserTypeName = c.String(nullable: false, maxLength: 50),
                    })
                .PrimaryKey(t => t.UserTypeID);
            
            CreateTable(
                "dbo.ProductModel",
                c => new
                    {
                        ModelID = c.Guid(nullable: false),
                        ModelName = c.String(nullable: false, maxLength: 250),
                        MakeID = c.Guid(nullable: false),
                        CategoryID = c.Guid(nullable: false),
                        IsActive = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.ModelID)
                .ForeignKey("dbo.Category", t => t.CategoryID, cascadeDelete: true)
                .ForeignKey("dbo.Make", t => t.MakeID, cascadeDelete: true)
                .Index(t => t.MakeID)
                .Index(t => t.CategoryID);
            
            CreateTable(
                "dbo.Category",
                c => new
                    {
                        CategoryID = c.Guid(nullable: false),
                        SubCategoryID = c.Guid(),
                        CategoryName = c.String(nullable: false, maxLength: 50),
                        Description = c.String(nullable: false, maxLength: 250),
                        IsActive = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.CategoryID)
                .ForeignKey("dbo.Category", t => t.SubCategoryID)
                .Index(t => t.SubCategoryID);
            
            CreateTable(
                "dbo.Make",
                c => new
                    {
                        MakeID = c.Guid(nullable: false),
                        MakeName = c.String(nullable: false, maxLength: 50),
                        IsActive = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.MakeID);
            
            CreateTable(
                "dbo.Property",
                c => new
                    {
                        PropertyID = c.Guid(nullable: false),
                        PropertyName = c.String(nullable: false, maxLength: 50),
                        IsActive = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.PropertyID);
            
            CreateTable(
                "dbo.ProductTechnicProperty",
                c => new
                    {
                        ProductPropertyID = c.Guid(nullable: false),
                        ProductID = c.Guid(nullable: false),
                        PropertyID = c.Guid(nullable: false),
                        PropertyValue = c.String(nullable: false, maxLength: 50),
                    })
                .PrimaryKey(t => t.ProductPropertyID)
                .ForeignKey("dbo.Product", t => t.ProductID, cascadeDelete: true)
                .ForeignKey("dbo.Property", t => t.PropertyID, cascadeDelete: true)
                .Index(t => t.ProductID)
                .Index(t => t.PropertyID);
            
            CreateTable(
                "dbo.ProductPicture",
                c => new
                    {
                        ProductPictureID = c.Guid(nullable: false),
                        ProductID = c.Guid(nullable: false),
                        PicturePath = c.String(nullable: false, maxLength: 250),
                        IsActive = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.ProductPictureID)
                .ForeignKey("dbo.Product", t => t.ProductID, cascadeDelete: true)
                .Index(t => t.ProductID);
            
            CreateTable(
                "dbo.CampaignProducts",
                c => new
                    {
                        Campaign_CampaignID = c.Guid(nullable: false),
                        Product_ProductID = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => new { t.Campaign_CampaignID, t.Product_ProductID })
                .ForeignKey("dbo.Campaigns", t => t.Campaign_CampaignID, cascadeDelete: true)
                .ForeignKey("dbo.Product", t => t.Product_ProductID, cascadeDelete: true)
                .Index(t => t.Campaign_CampaignID)
                .Index(t => t.Product_ProductID);
            
            CreateTable(
                "dbo.MakeCategories",
                c => new
                    {
                        Make_MakeID = c.Guid(nullable: false),
                        Category_CategoryID = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => new { t.Make_MakeID, t.Category_CategoryID })
                .ForeignKey("dbo.Make", t => t.Make_MakeID, cascadeDelete: true)
                .ForeignKey("dbo.Category", t => t.Category_CategoryID, cascadeDelete: true)
                .Index(t => t.Make_MakeID)
                .Index(t => t.Category_CategoryID);
            
            CreateTable(
                "dbo.CategoryProperty",
                c => new
                    {
                        CategoryID = c.Guid(nullable: false),
                        PropertyID = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => new { t.CategoryID, t.PropertyID })
                .ForeignKey("dbo.Category", t => t.CategoryID, cascadeDelete: true)
                .ForeignKey("dbo.Property", t => t.PropertyID, cascadeDelete: true)
                .Index(t => t.CategoryID)
                .Index(t => t.PropertyID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.BasketProduct", "UserID", "dbo.User");
            DropForeignKey("dbo.BasketProduct", "ProductID", "dbo.Product");
            DropForeignKey("dbo.ProductPicture", "ProductID", "dbo.Product");
            DropForeignKey("dbo.Product", "ModelID", "dbo.ProductModel");
            DropForeignKey("dbo.ProductModel", "MakeID", "dbo.Make");
            DropForeignKey("dbo.ProductModel", "CategoryID", "dbo.Category");
            DropForeignKey("dbo.Category", "SubCategoryID", "dbo.Category");
            DropForeignKey("dbo.CategoryProperty", "PropertyID", "dbo.Property");
            DropForeignKey("dbo.CategoryProperty", "CategoryID", "dbo.Category");
            DropForeignKey("dbo.ProductTechnicProperty", "PropertyID", "dbo.Property");
            DropForeignKey("dbo.ProductTechnicProperty", "ProductID", "dbo.Product");
            DropForeignKey("dbo.MakeCategories", "Category_CategoryID", "dbo.Category");
            DropForeignKey("dbo.MakeCategories", "Make_MakeID", "dbo.Make");
            DropForeignKey("dbo.OrderDetail", "ProductID", "dbo.Product");
            DropForeignKey("dbo.OrderDetail", "OrderID", "dbo.Order");
            DropForeignKey("dbo.Order", "UserDetailID", "dbo.UserDetail");
            DropForeignKey("dbo.Order", "UserCardID", "dbo.UserCard");
            DropForeignKey("dbo.Order", "AddressID", "dbo.UserAddress");
            DropForeignKey("dbo.Order", "UserID", "dbo.User");
            DropForeignKey("dbo.User", "UserTypeID", "dbo.UserType");
            DropForeignKey("dbo.UserDetail", "UserID", "dbo.User");
            DropForeignKey("dbo.UserCard", "UserID", "dbo.User");
            DropForeignKey("dbo.UserAddress", "UserID", "dbo.User");
            DropForeignKey("dbo.UserAddress", "DistrictID", "dbo.District");
            DropForeignKey("dbo.District", "CityID", "dbo.City");
            DropForeignKey("dbo.ProductComment", "UserID", "dbo.User");
            DropForeignKey("dbo.ProductComment", "ProductID", "dbo.Product");
            DropForeignKey("dbo.Order", "PaymentTypeID", "dbo.PaymentType");
            DropForeignKey("dbo.CampaignProducts", "Product_ProductID", "dbo.Product");
            DropForeignKey("dbo.CampaignProducts", "Campaign_CampaignID", "dbo.Campaigns");
            DropIndex("dbo.CategoryProperty", new[] { "PropertyID" });
            DropIndex("dbo.CategoryProperty", new[] { "CategoryID" });
            DropIndex("dbo.MakeCategories", new[] { "Category_CategoryID" });
            DropIndex("dbo.MakeCategories", new[] { "Make_MakeID" });
            DropIndex("dbo.CampaignProducts", new[] { "Product_ProductID" });
            DropIndex("dbo.CampaignProducts", new[] { "Campaign_CampaignID" });
            DropIndex("dbo.ProductPicture", new[] { "ProductID" });
            DropIndex("dbo.ProductTechnicProperty", new[] { "PropertyID" });
            DropIndex("dbo.ProductTechnicProperty", new[] { "ProductID" });
            DropIndex("dbo.Category", new[] { "SubCategoryID" });
            DropIndex("dbo.ProductModel", new[] { "CategoryID" });
            DropIndex("dbo.ProductModel", new[] { "MakeID" });
            DropIndex("dbo.UserDetail", new[] { "UserID" });
            DropIndex("dbo.UserCard", new[] { "UserID" });
            DropIndex("dbo.District", new[] { "CityID" });
            DropIndex("dbo.UserAddress", new[] { "DistrictID" });
            DropIndex("dbo.UserAddress", new[] { "UserID" });
            DropIndex("dbo.ProductComment", new[] { "ProductID" });
            DropIndex("dbo.ProductComment", new[] { "UserID" });
            DropIndex("dbo.User", new[] { "UserTypeID" });
            DropIndex("dbo.Order", new[] { "UserCardID" });
            DropIndex("dbo.Order", new[] { "PaymentTypeID" });
            DropIndex("dbo.Order", new[] { "AddressID" });
            DropIndex("dbo.Order", new[] { "UserDetailID" });
            DropIndex("dbo.Order", new[] { "UserID" });
            DropIndex("dbo.OrderDetail", new[] { "ProductID" });
            DropIndex("dbo.OrderDetail", new[] { "OrderID" });
            DropIndex("dbo.Product", new[] { "ModelID" });
            DropIndex("dbo.BasketProduct", new[] { "ProductID" });
            DropIndex("dbo.BasketProduct", new[] { "UserID" });
            DropTable("dbo.CategoryProperty");
            DropTable("dbo.MakeCategories");
            DropTable("dbo.CampaignProducts");
            DropTable("dbo.ProductPicture");
            DropTable("dbo.ProductTechnicProperty");
            DropTable("dbo.Property");
            DropTable("dbo.Make");
            DropTable("dbo.Category");
            DropTable("dbo.ProductModel");
            DropTable("dbo.UserType");
            DropTable("dbo.UserDetail");
            DropTable("dbo.UserCard");
            DropTable("dbo.City");
            DropTable("dbo.District");
            DropTable("dbo.UserAddress");
            DropTable("dbo.ProductComment");
            DropTable("dbo.User");
            DropTable("dbo.PaymentType");
            DropTable("dbo.Order");
            DropTable("dbo.OrderDetail");
            DropTable("dbo.Campaigns");
            DropTable("dbo.Product");
            DropTable("dbo.BasketProduct");
        }
    }
}
