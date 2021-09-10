namespace DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class New : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.OrderDetails", "product_Id", c => c.Int());
            CreateIndex("dbo.OrderDetails", "product_Id");
            AddForeignKey("dbo.OrderDetails", "product_Id", "dbo.Products", "Id");
            DropColumn("dbo.OrderDetails", "ProductId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.OrderDetails", "ProductId", c => c.Int(nullable: false));
            DropForeignKey("dbo.OrderDetails", "product_Id", "dbo.Products");
            DropIndex("dbo.OrderDetails", new[] { "product_Id" });
            DropColumn("dbo.OrderDetails", "product_Id");
        }
    }
}
