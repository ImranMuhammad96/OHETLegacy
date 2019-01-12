namespace OHET_Project.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PostFix : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Posts", "isPublic", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Posts", "isPublic");
        }
    }
}
