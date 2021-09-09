namespace DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class First : DbMigration
    {
        public override void Up()
        {
            MoveTable(name: "public.Orders", newSchema: "dbo");
            MoveTable(name: "public.OrderDetails", newSchema: "dbo");
            MoveTable(name: "public.Users", newSchema: "dbo");
            CreateTable(
                "dbo.Baskets",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ProductId = c.Int(nullable: false),
                        Quantity = c.Int(nullable: false),
                        Price = c.Int(nullable: false),
                        UserId = c.Int(nullable: false),
                        ProductPrice = c.Int(nullable: false),
                        ProductName = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Categories",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Surname = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Products",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Code = c.String(),
                        Name = c.String(),
                        Price = c.String(),
                        ImagePath = c.String(),
                        Stock = c.String(),
                        Status = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Products");
            DropTable("dbo.Categories");
            DropTable("dbo.Baskets");
            MoveTable(name: "dbo.Users", newSchema: "public");
            MoveTable(name: "dbo.OrderDetails", newSchema: "public");
            MoveTable(name: "dbo.Orders", newSchema: "public");
        }
    }
}
