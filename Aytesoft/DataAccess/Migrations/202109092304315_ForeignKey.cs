namespace DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ForeignKey : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.OrderDetails", "product_Id", "dbo.Products");
            DropIndex("dbo.OrderDetails", new[] { "product_Id" });
            AlterColumn("dbo.OrderDetails", "product_Id", c => c.Int(nullable: false));
            CreateIndex("dbo.OrderDetails", "product_Id");
            AddForeignKey("dbo.OrderDetails", "product_Id", "dbo.Products", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.OrderDetails", "product_Id", "dbo.Products");
            DropIndex("dbo.OrderDetails", new[] { "product_Id" });
            AlterColumn("dbo.OrderDetails", "product_Id", c => c.Int());
            CreateIndex("dbo.OrderDetails", "product_Id");
            AddForeignKey("dbo.OrderDetails", "product_Id", "dbo.Products", "Id");
        }
    }
}
