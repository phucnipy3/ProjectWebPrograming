namespace MVC.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Comment",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Content = c.String(),
                        CreateDate = c.DateTime(),
                        CreateBy = c.Int(),
                        ProductID = c.Int(),
                        ParentID = c.Int(),
                        Status = c.Boolean(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Customer",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        UserID = c.String(maxLength: 50, unicode: false),
                        Password = c.String(maxLength: 250, unicode: false),
                        Name = c.String(maxLength: 250),
                        Address = c.String(maxLength: 250),
                        Email = c.String(maxLength: 250, unicode: false),
                        Phone = c.String(maxLength: 10, unicode: false),
                        Status = c.Boolean(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Order",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        CreateDate = c.DateTime(),
                        CreateBy = c.Int(),
                        ShipName = c.String(maxLength: 250),
                        ShipMobile = c.String(maxLength: 10, unicode: false),
                        ShipAddress = c.String(maxLength: 250),
                        ShipEmail = c.String(maxLength: 250),
                        Ordered = c.Boolean(),
                        Complete = c.Boolean(),
                        Status = c.Boolean(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.ProductCategory",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(maxLength: 250),
                        MetaTitle = c.String(maxLength: 250, unicode: false),
                        ParentID = c.Int(),
                        DisplayOrder = c.Int(),
                        SeoTitle = c.String(maxLength: 50),
                        CreatedDate = c.DateTime(),
                        CreatedBy = c.Int(),
                        ModifiedDate = c.DateTime(),
                        ModifiedBy = c.Int(),
                        Status = c.Boolean(),
                        ShowOnHome = c.Boolean(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Product",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(maxLength: 250),
                        MetaTitle = c.String(maxLength: 250, unicode: false),
                        Author = c.String(maxLength: 250),
                        Image = c.String(maxLength: 250, unicode: false),
                        Price = c.Decimal(precision: 18, scale: 0),
                        PromotionPrice = c.Decimal(precision: 18, scale: 0),
                        IncludedVAT = c.Boolean(),
                        Quantity = c.Int(),
                        CategoryID = c.Int(),
                        Detail = c.String(storeType: "ntext"),
                        Warranty = c.Int(),
                        CreatedDate = c.DateTime(),
                        CreatedBy = c.Int(),
                        ModifiedDate = c.DateTime(),
                        ModifiedBy = c.Int(),
                        Status = c.Boolean(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Rate",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        RatePoint = c.Int(),
                        CreateDate = c.DateTime(),
                        CreateBy = c.Int(),
                        ProductID = c.Int(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        UserID = c.String(maxLength: 50, unicode: false),
                        Password = c.String(maxLength: 250, unicode: false),
                        Name = c.String(maxLength: 250),
                        Address = c.String(maxLength: 250),
                        Email = c.String(maxLength: 250, unicode: false),
                        Phone = c.String(maxLength: 10, unicode: false),
                        Sex = c.String(),
                        Status = c.Boolean(),
                    })
                .PrimaryKey(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Users");
            DropTable("dbo.Rate");
            DropTable("dbo.Product");
            DropTable("dbo.ProductCategory");
            DropTable("dbo.Order");
            DropTable("dbo.Customer");
            DropTable("dbo.Comment");
        }
    }
}
