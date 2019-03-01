namespace OnlineStore.WebUI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangeTables : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.SelectedClothes", "ClothIdId", c => c.Int());
            AddColumn("dbo.WishList", "ClothId", c => c.Int());
            DropColumn("dbo.WishList", "Number");
        }
        
        public override void Down()
        {
            AddColumn("dbo.WishList", "Number", c => c.Int(nullable: false));
            DropColumn("dbo.WishList", "ClothId");
            DropColumn("dbo.SelectedClothes", "ClothIdId");
        }
    }
}
