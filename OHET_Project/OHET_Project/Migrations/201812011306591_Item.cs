namespace OHET_Project.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Item : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Items",
                c => new
                    {
                        IDItem = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        Cost = c.Int(nullable: false),
                        Notes = c.String(nullable: false),
                        IDContent = c.Int(),
                    })
                .PrimaryKey(t => t.IDItem)
                .ForeignKey("dbo.Contents", t => t.IDContent)
                .Index(t => t.IDContent);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Items", "IDContent", "dbo.Contents");
            DropIndex("dbo.Items", new[] { "IDContent" });
            DropTable("dbo.Items");
        }
    }
}
