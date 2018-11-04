namespace OHET_Project.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Init2 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Contents", "ApplicationUserId", c => c.String(maxLength: 128));
            CreateIndex("dbo.Contents", "ApplicationUserId");
            AddForeignKey("dbo.Contents", "ApplicationUserId", "dbo.AspNetUsers", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Contents", "ApplicationUserId", "dbo.AspNetUsers");
            DropIndex("dbo.Contents", new[] { "ApplicationUserId" });
            DropColumn("dbo.Contents", "ApplicationUserId");
        }
    }
}
