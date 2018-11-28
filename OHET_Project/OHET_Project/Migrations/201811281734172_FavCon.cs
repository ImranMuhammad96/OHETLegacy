namespace OHET_Project.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FavCon : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.FavCons",
                c => new
                    {
                        IDFavCon = c.Int(nullable: false, identity: true),
                        ApplicationUserId = c.String(maxLength: 128),
                        IDContent = c.Int(),
                    })
                .PrimaryKey(t => t.IDFavCon)
                .ForeignKey("dbo.AspNetUsers", t => t.ApplicationUserId)
                .ForeignKey("dbo.Contents", t => t.IDContent)
                .Index(t => t.ApplicationUserId)
                .Index(t => t.IDContent);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.FavCons", "IDContent", "dbo.Contents");
            DropForeignKey("dbo.FavCons", "ApplicationUserId", "dbo.AspNetUsers");
            DropIndex("dbo.FavCons", new[] { "IDContent" });
            DropIndex("dbo.FavCons", new[] { "ApplicationUserId" });
            DropTable("dbo.FavCons");
        }
    }
}
