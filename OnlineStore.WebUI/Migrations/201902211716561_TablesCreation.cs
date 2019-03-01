namespace OnlineStore.WebUI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class TablesCreation : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ClothesHeap",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        WhishId = c.Int(),
                        SelectedId = c.Int(),
                        Name = c.String(),
                        Number = c.Int(nullable: false),
                        Color = c.String(),
                        Size = c.String(),
                        Wish_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.SelectedClothes", t => t.SelectedId)
                .ForeignKey("dbo.WishList", t => t.Wish_Id)
                .Index(t => t.SelectedId)
                .Index(t => t.Wish_Id);
            
            CreateTable(
                "dbo.SelectedClothes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.WishList",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Color",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Hex = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.ShopItem",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Description = c.String(),
                        TitleImg = c.String(),
                        Gender = c.String(),
                        Price = c.Double(nullable: false),
                        Quantity = c.Int(nullable: false),
                        Name = c.String(),
                        Number = c.Int(nullable: false),
                        Color = c.String(),
                        Size = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.ItemImage",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Path = c.String(),
                        ShopItemId = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ShopItem", t => t.ShopItemId)
                .Index(t => t.ShopItemId);
            
            CreateTable(
                "dbo.Size",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.UserAccount",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FullName = c.String(nullable: false),
                        DateOfBirth = c.DateTime(nullable: false),
                        Gender = c.String(),
                        Email = c.String(nullable: false),
                        Password = c.String(nullable: false),
                        Role = c.String(),
                        IsVerified = c.Boolean(nullable: false),
                        ActivationCode = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.ShopItemColor",
                c => new
                    {
                        ShopItem_Id = c.Int(nullable: false),
                        Color_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.ShopItem_Id, t.Color_Id })
                .ForeignKey("dbo.ShopItem", t => t.ShopItem_Id, cascadeDelete: true)
                .ForeignKey("dbo.Color", t => t.Color_Id, cascadeDelete: true)
                .Index(t => t.ShopItem_Id)
                .Index(t => t.Color_Id);
            
            CreateTable(
                "dbo.SizeShopItem",
                c => new
                    {
                        Size_Id = c.Int(nullable: false),
                        ShopItem_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Size_Id, t.ShopItem_Id })
                .ForeignKey("dbo.Size", t => t.Size_Id, cascadeDelete: true)
                .ForeignKey("dbo.ShopItem", t => t.ShopItem_Id, cascadeDelete: true)
                .Index(t => t.Size_Id)
                .Index(t => t.ShopItem_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.SizeShopItem", "ShopItem_Id", "dbo.ShopItem");
            DropForeignKey("dbo.SizeShopItem", "Size_Id", "dbo.Size");
            DropForeignKey("dbo.ItemImage", "ShopItemId", "dbo.ShopItem");
            DropForeignKey("dbo.ShopItemColor", "Color_Id", "dbo.Color");
            DropForeignKey("dbo.ShopItemColor", "ShopItem_Id", "dbo.ShopItem");
            DropForeignKey("dbo.ClothesHeap", "Wish_Id", "dbo.WishList");
            DropForeignKey("dbo.ClothesHeap", "SelectedId", "dbo.SelectedClothes");
            DropIndex("dbo.SizeShopItem", new[] { "ShopItem_Id" });
            DropIndex("dbo.SizeShopItem", new[] { "Size_Id" });
            DropIndex("dbo.ShopItemColor", new[] { "Color_Id" });
            DropIndex("dbo.ShopItemColor", new[] { "ShopItem_Id" });
            DropIndex("dbo.ItemImage", new[] { "ShopItemId" });
            DropIndex("dbo.ClothesHeap", new[] { "Wish_Id" });
            DropIndex("dbo.ClothesHeap", new[] { "SelectedId" });
            DropTable("dbo.SizeShopItem");
            DropTable("dbo.ShopItemColor");
            DropTable("dbo.UserAccount");
            DropTable("dbo.Size");
            DropTable("dbo.ItemImage");
            DropTable("dbo.ShopItem");
            DropTable("dbo.Color");
            DropTable("dbo.WishList");
            DropTable("dbo.SelectedClothes");
            DropTable("dbo.ClothesHeap");
        }
    }
}
