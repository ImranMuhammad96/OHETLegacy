namespace OHET_Project.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class QuickFix : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Abilities", "name");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Abilities", "name", c => c.String(nullable: false));
        }
    }
}
