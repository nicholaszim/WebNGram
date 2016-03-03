namespace DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CategoryExample_relationship : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Categories",
                c => new
                    {
                        CategoryId = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.CategoryId);
            
            CreateTable(
                "dbo.Examples",
                c => new
                    {
                        ExampleId = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.ExampleId);
            
            CreateTable(
                "dbo.Ngrams",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Key = c.String(),
                        Value = c.Int(nullable: false),
                        Example_ExampleId = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Examples", t => t.Example_ExampleId)
                .Index(t => t.Example_ExampleId);
            
            CreateTable(
                "dbo.ExampleCategories",
                c => new
                    {
                        Example_ExampleId = c.Int(nullable: false),
                        Category_CategoryId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Example_ExampleId, t.Category_CategoryId })
                .ForeignKey("dbo.Examples", t => t.Example_ExampleId, cascadeDelete: true)
                .ForeignKey("dbo.Categories", t => t.Category_CategoryId, cascadeDelete: true)
                .Index(t => t.Example_ExampleId)
                .Index(t => t.Category_CategoryId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Ngrams", "Example_ExampleId", "dbo.Examples");
            DropForeignKey("dbo.ExampleCategories", "Category_CategoryId", "dbo.Categories");
            DropForeignKey("dbo.ExampleCategories", "Example_ExampleId", "dbo.Examples");
            DropIndex("dbo.ExampleCategories", new[] { "Category_CategoryId" });
            DropIndex("dbo.ExampleCategories", new[] { "Example_ExampleId" });
            DropIndex("dbo.Ngrams", new[] { "Example_ExampleId" });
            DropTable("dbo.ExampleCategories");
            DropTable("dbo.Ngrams");
            DropTable("dbo.Examples");
            DropTable("dbo.Categories");
        }
    }
}
