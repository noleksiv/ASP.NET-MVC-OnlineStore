namespace OnlineStore.WebUI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ColumnRename : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.SelectedClothes", "ClothId", c => c.Int());
            DropColumn("dbo.SelectedClothes", "ClothIdId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.SelectedClothes", "ClothIdId", c => c.Int());
            DropColumn("dbo.SelectedClothes", "ClothId");
        }
    }
}
