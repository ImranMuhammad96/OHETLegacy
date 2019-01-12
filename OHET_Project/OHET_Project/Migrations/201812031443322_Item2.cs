namespace OHET_Project.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Item2 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Comments",
                c => new
                    {
                        IDComment = c.Int(nullable: false, identity: true),
                        Description = c.String(nullable: false),
                        Date = c.DateTime(nullable: false),
                        ApplicationUserId = c.String(maxLength: 128),
                        IDPost = c.Int(),
                    })
                .PrimaryKey(t => t.IDComment)
                .ForeignKey("dbo.AspNetUsers", t => t.ApplicationUserId)
                .ForeignKey("dbo.Posts", t => t.IDPost)
                .Index(t => t.ApplicationUserId)
                .Index(t => t.IDPost);
            
            CreateTable(
                "dbo.Posts",
                c => new
                    {
                        IDPost = c.Int(nullable: false, identity: true),
                        Title = c.String(nullable: false),
                        Description = c.String(),
                        Date = c.DateTime(nullable: false),
                        ApplicationUserId = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.IDPost)
                .ForeignKey("dbo.AspNetUsers", t => t.ApplicationUserId)
                .Index(t => t.ApplicationUserId);
            
            CreateTable(
                "dbo.Subposts",
                c => new
                    {
                        IDSubpost = c.Int(nullable: false, identity: true),
                        Title = c.String(nullable: false),
                        Description = c.String(),
                        OrderNr = c.Int(nullable: false),
                        IDPost = c.Int(),
                    })
                .PrimaryKey(t => t.IDSubpost)
                .ForeignKey("dbo.Posts", t => t.IDPost)
                .Index(t => t.IDPost);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Subposts", "IDPost", "dbo.Posts");
            DropForeignKey("dbo.Comments", "IDPost", "dbo.Posts");
            DropForeignKey("dbo.Posts", "ApplicationUserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Comments", "ApplicationUserId", "dbo.AspNetUsers");
            DropIndex("dbo.Subposts", new[] { "IDPost" });
            DropIndex("dbo.Posts", new[] { "ApplicationUserId" });
            DropIndex("dbo.Comments", new[] { "IDPost" });
            DropIndex("dbo.Comments", new[] { "ApplicationUserId" });
            DropTable("dbo.Subposts");
            DropTable("dbo.Posts");
            DropTable("dbo.Comments");
        }
    }
}
