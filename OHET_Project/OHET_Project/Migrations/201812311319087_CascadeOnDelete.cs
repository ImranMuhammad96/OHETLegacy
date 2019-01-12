namespace OHET_Project.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CascadeOnDelete : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Comments", "IDPost", "dbo.Posts");
            DropForeignKey("dbo.Subposts", "IDPost", "dbo.Posts");
            DropIndex("dbo.Comments", new[] { "IDPost" });
            DropIndex("dbo.Subposts", new[] { "IDPost" });
            AlterColumn("dbo.Comments", "IDPost", c => c.Int(nullable: false));
            AlterColumn("dbo.Subposts", "IDPost", c => c.Int(nullable: false));
            CreateIndex("dbo.Comments", "IDPost");
            CreateIndex("dbo.Subposts", "IDPost");
            AddForeignKey("dbo.Comments", "IDPost", "dbo.Posts", "IDPost", cascadeDelete: true);
            AddForeignKey("dbo.Subposts", "IDPost", "dbo.Posts", "IDPost", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Subposts", "IDPost", "dbo.Posts");
            DropForeignKey("dbo.Comments", "IDPost", "dbo.Posts");
            DropIndex("dbo.Subposts", new[] { "IDPost" });
            DropIndex("dbo.Comments", new[] { "IDPost" });
            AlterColumn("dbo.Subposts", "IDPost", c => c.Int());
            AlterColumn("dbo.Comments", "IDPost", c => c.Int());
            CreateIndex("dbo.Subposts", "IDPost");
            CreateIndex("dbo.Comments", "IDPost");
            AddForeignKey("dbo.Subposts", "IDPost", "dbo.Posts", "IDPost");
            AddForeignKey("dbo.Comments", "IDPost", "dbo.Posts", "IDPost");
        }
    }
}
