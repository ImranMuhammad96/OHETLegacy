namespace OHET_Project.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ClassFix : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Classes", "isSpellcaster", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Classes", "isSpellcaster");
        }
    }
}
