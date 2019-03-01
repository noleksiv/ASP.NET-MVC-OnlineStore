namespace OnlineStore.WebUI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class TableWishSelectedChanged : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.ClothesHeap", "SelectedId", "dbo.SelectedClothes");
            DropForeignKey("dbo.ClothesHeap", "Wish_Id", "dbo.WishList");
            DropIndex("dbo.ClothesHeap", new[] { "SelectedId" });
            DropIndex("dbo.ClothesHeap", new[] { "Wish_Id" });
            AddColumn("dbo.SelectedClothes", "Name", c => c.String());
            AddColumn("dbo.SelectedClothes", "Number", c => c.Int(nullable: false));
            AddColumn("dbo.SelectedClothes", "Color", c => c.String());
            AddColumn("dbo.SelectedClothes", "Size", c => c.String());
            AddColumn("dbo.WishList", "Name", c => c.String());
            AddColumn("dbo.WishList", "Number", c => c.Int(nullable: false));
            AddColumn("dbo.WishList", "Color", c => c.String());
            AddColumn("dbo.WishList", "Size", c => c.String());
            AlterColumn("dbo.SelectedClothes", "UserId", c => c.Int());
            AlterColumn("dbo.WishList", "UserId", c => c.Int());
            DropTable("dbo.ClothesHeap");
        }
        
        public override void Down()
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
                .PrimaryKey(t => t.Id);
            
            AlterColumn("dbo.WishList", "UserId", c => c.Int(nullable: false));
            AlterColumn("dbo.SelectedClothes", "UserId", c => c.Int(nullable: false));
            DropColumn("dbo.WishList", "Size");
            DropColumn("dbo.WishList", "Color");
            DropColumn("dbo.WishList", "Number");
            DropColumn("dbo.WishList", "Name");
            DropColumn("dbo.SelectedClothes", "Size");
            DropColumn("dbo.SelectedClothes", "Color");
            DropColumn("dbo.SelectedClothes", "Number");
            DropColumn("dbo.SelectedClothes", "Name");
            CreateIndex("dbo.ClothesHeap", "Wish_Id");
            CreateIndex("dbo.ClothesHeap", "SelectedId");
            AddForeignKey("dbo.ClothesHeap", "Wish_Id", "dbo.WishList", "Id");
            AddForeignKey("dbo.ClothesHeap", "SelectedId", "dbo.SelectedClothes", "Id");
        }
    }
}
