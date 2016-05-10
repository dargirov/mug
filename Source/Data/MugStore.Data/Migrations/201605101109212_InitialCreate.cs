namespace MugStore.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Cities",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        PostCode = c.String(),
                        CreatedOn = c.DateTime(nullable: false),
                        ModifiedOn = c.DateTime(),
                        IsDeleted = c.Boolean(nullable: false),
                        DeletedOn = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Images",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Path = c.String(),
                        Width = c.Int(nullable: false),
                        Height = c.Int(nullable: false),
                        Dpi = c.Int(nullable: false),
                        Rotation = c.Double(nullable: false),
                        Y = c.Double(nullable: false),
                        sdasdf = c.Int(nullable: false),
                        CreatedOn = c.DateTime(nullable: false),
                        ModifiedOn = c.DateTime(),
                        IsDeleted = c.Boolean(nullable: false),
                        DeletedOn = c.DateTime(),
                        Order_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Orders", t => t.Order_Id)
                .Index(t => t.Order_Id);
            
            CreateTable(
                "dbo.Orders",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Quantity = c.Int(),
                        PaymentMethod = c.Int(),
                        DeliveryInfoId = c.Int(nullable: false),
                        CreatedOn = c.DateTime(nullable: false),
                        ModifiedOn = c.DateTime(),
                        IsDeleted = c.Boolean(nullable: false),
                        DeletedOn = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.DeliveryInfoes", t => t.DeliveryInfoId, cascadeDelete: true)
                .Index(t => t.DeliveryInfoId);
            
            CreateTable(
                "dbo.DeliveryInfoes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FullName = c.String(),
                        Phone = c.String(),
                        CityId = c.Int(),
                        Address = c.String(),
                        Comment = c.String(),
                        CreatedOn = c.DateTime(nullable: false),
                        ModifiedOn = c.DateTime(),
                        IsDeleted = c.Boolean(nullable: false),
                        DeletedOn = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Cities", t => t.CityId)
                .Index(t => t.CityId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Images", "Order_Id", "dbo.Orders");
            DropForeignKey("dbo.Orders", "DeliveryInfoId", "dbo.DeliveryInfoes");
            DropForeignKey("dbo.DeliveryInfoes", "CityId", "dbo.Cities");
            DropIndex("dbo.DeliveryInfoes", new[] { "CityId" });
            DropIndex("dbo.Orders", new[] { "DeliveryInfoId" });
            DropIndex("dbo.Images", new[] { "Order_Id" });
            DropTable("dbo.DeliveryInfoes");
            DropTable("dbo.Orders");
            DropTable("dbo.Images");
            DropTable("dbo.Cities");
        }
    }
}
