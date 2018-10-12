namespace OHET_Project.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialD : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Abilities",
                c => new
                    {
                        IDAbility = c.Int(nullable: false, identity: true),
                        name = c.String(nullable: false),
                        description = c.String(nullable: false),
                        conceptLvl = c.Int(nullable: false),
                        IDClass = c.Int(),
                    })
                .PrimaryKey(t => t.IDAbility)
                .ForeignKey("dbo.Classes", t => t.IDClass)
                .Index(t => t.IDClass);
            
            CreateTable(
                "dbo.Classes",
                c => new
                    {
                        IDClass = c.Int(nullable: false, identity: true),
                        name = c.String(nullable: false),
                        description = c.String(),
                        IDContent = c.Int(),
                        Discriminator = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.IDClass)
                .ForeignKey("dbo.Contents", t => t.IDContent)
                .Index(t => t.IDContent);
            
            CreateTable(
                "dbo.Contents",
                c => new
                    {
                        IDContent = c.Int(nullable: false, identity: true),
                        isOfficial = c.Boolean(nullable: false),
                        isPublic = c.Boolean(nullable: false),
                        ApplicationUserId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.IDContent)
                .ForeignKey("dbo.AspNetUsers", t => t.ApplicationUserId, cascadeDelete: true)
                .Index(t => t.ApplicationUserId);
            
            CreateTable(
                "dbo.Adventures",
                c => new
                    {
                        IDAdventure = c.Int(nullable: false, identity: true),
                        title = c.String(nullable: false),
                        description = c.String(nullable: false),
                        IDContent = c.Int(),
                    })
                .PrimaryKey(t => t.IDAdventure)
                .ForeignKey("dbo.Contents", t => t.IDContent)
                .Index(t => t.IDContent);
            
            CreateTable(
                "dbo.AspNetUsers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Email = c.String(maxLength: 256),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(nullable: false, maxLength: 256),
                        Content_IDContent = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Contents", t => t.Content_IDContent)
                .Index(t => t.UserName, unique: true, name: "UserNameIndex")
                .Index(t => t.Content_IDContent);
            
            CreateTable(
                "dbo.AspNetUserClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(nullable: false, maxLength: 128),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserLogins",
                c => new
                    {
                        LoginProvider = c.String(nullable: false, maxLength: 128),
                        ProviderKey = c.String(nullable: false, maxLength: 128),
                        UserId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserRoles",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        RoleId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetRoles", t => t.RoleId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.Heroes",
                c => new
                    {
                        IDHero = c.Int(nullable: false, identity: true),
                        name = c.String(nullable: false),
                        conceptLvl = c.Int(nullable: false),
                        description = c.String(nullable: false),
                        StrAttribute = c.Int(nullable: false),
                        DexAttribute = c.Int(nullable: false),
                        ConAttribute = c.Int(nullable: false),
                        IntAttribute = c.Int(nullable: false),
                        WisAttribute = c.Int(nullable: false),
                        ChaAttribute = c.Int(nullable: false),
                        gold = c.Int(nullable: false),
                        exp = c.Int(nullable: false),
                        IDContent = c.Int(),
                    })
                .PrimaryKey(t => t.IDHero)
                .ForeignKey("dbo.Contents", t => t.IDContent)
                .Index(t => t.IDContent);
            
            CreateTable(
                "dbo.Monsters",
                c => new
                    {
                        IDMonster = c.Int(nullable: false, identity: true),
                        name = c.String(nullable: false),
                        description = c.String(nullable: false),
                        conceptLvl = c.Int(nullable: false),
                        IDContent = c.Int(),
                    })
                .PrimaryKey(t => t.IDMonster)
                .ForeignKey("dbo.Contents", t => t.IDContent)
                .Index(t => t.IDContent);
            
            CreateTable(
                "dbo.Rules",
                c => new
                    {
                        IDRule = c.Int(nullable: false, identity: true),
                        title = c.String(nullable: false),
                        description = c.String(nullable: false),
                        IDContent = c.Int(),
                    })
                .PrimaryKey(t => t.IDRule)
                .ForeignKey("dbo.Contents", t => t.IDContent)
                .Index(t => t.IDContent);
            
            CreateTable(
                "dbo.Spells",
                c => new
                    {
                        IDSpell = c.Int(nullable: false, identity: true),
                        name = c.String(nullable: false),
                        description = c.String(nullable: false),
                        conceptLvl = c.Int(nullable: false),
                        IDContent = c.Int(),
                        IDClass = c.Int(),
                    })
                .PrimaryKey(t => t.IDSpell)
                .ForeignKey("dbo.Classes", t => t.IDClass)
                .ForeignKey("dbo.Contents", t => t.IDContent)
                .Index(t => t.IDContent)
                .Index(t => t.IDClass);
            
            CreateTable(
                "dbo.AspNetRoles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");
            
            CreateTable(
                "dbo.CH",
                c => new
                    {
                        HeroRefId = c.Int(nullable: false),
                        ClassRefId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.HeroRefId, t.ClassRefId })
                .ForeignKey("dbo.Heroes", t => t.HeroRefId, cascadeDelete: true)
                .ForeignKey("dbo.Classes", t => t.ClassRefId, cascadeDelete: true)
                .Index(t => t.HeroRefId)
                .Index(t => t.ClassRefId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.Spells", "IDContent", "dbo.Contents");
            DropForeignKey("dbo.Spells", "IDClass", "dbo.Classes");
            DropForeignKey("dbo.Rules", "IDContent", "dbo.Contents");
            DropForeignKey("dbo.Monsters", "IDContent", "dbo.Contents");
            DropForeignKey("dbo.Heroes", "IDContent", "dbo.Contents");
            DropForeignKey("dbo.CH", "ClassRefId", "dbo.Classes");
            DropForeignKey("dbo.CH", "HeroRefId", "dbo.Heroes");
            DropForeignKey("dbo.Classes", "IDContent", "dbo.Contents");
            DropForeignKey("dbo.AspNetUsers", "Content_IDContent", "dbo.Contents");
            DropForeignKey("dbo.Contents", "ApplicationUserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Adventures", "IDContent", "dbo.Contents");
            DropForeignKey("dbo.Abilities", "IDClass", "dbo.Classes");
            DropIndex("dbo.CH", new[] { "ClassRefId" });
            DropIndex("dbo.CH", new[] { "HeroRefId" });
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropIndex("dbo.Spells", new[] { "IDClass" });
            DropIndex("dbo.Spells", new[] { "IDContent" });
            DropIndex("dbo.Rules", new[] { "IDContent" });
            DropIndex("dbo.Monsters", new[] { "IDContent" });
            DropIndex("dbo.Heroes", new[] { "IDContent" });
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.AspNetUsers", new[] { "Content_IDContent" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropIndex("dbo.Adventures", new[] { "IDContent" });
            DropIndex("dbo.Contents", new[] { "ApplicationUserId" });
            DropIndex("dbo.Classes", new[] { "IDContent" });
            DropIndex("dbo.Abilities", new[] { "IDClass" });
            DropTable("dbo.CH");
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.Spells");
            DropTable("dbo.Rules");
            DropTable("dbo.Monsters");
            DropTable("dbo.Heroes");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.Adventures");
            DropTable("dbo.Contents");
            DropTable("dbo.Classes");
            DropTable("dbo.Abilities");
        }
    }
}
