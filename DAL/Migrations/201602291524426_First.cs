namespace DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class First : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Examples",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Category = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Ngrams",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Key = c.String(),
                        Value = c.Int(nullable: false),
                        Example_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Examples", t => t.Example_Id)
                .Index(t => t.Example_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Ngrams", "Example_Id", "dbo.Examples");
            DropIndex("dbo.Ngrams", new[] { "Example_Id" });
            DropTable("dbo.Ngrams");
            DropTable("dbo.Examples");
        }
    }
}
