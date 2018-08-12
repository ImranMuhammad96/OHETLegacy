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
                "dbo.Items",
                c => new
                    {
                        IDItem = c.Int(nullable: false, identity: true),
                        name = c.String(nullable: false),
                        cost = c.Int(nullable: false),
                        description = c.String(nullable: false),
                        IDContent = c.Int(),
                        dr = c.Int(),
                        type = c.String(),
                        Discriminator = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.IDItem)
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
                        totalDR = c.Int(nullable: false),
                        gold = c.Int(nullable: false),
                        exp = c.Int(nullable: false),
                        IDConcept = c.Int(),
                        IDContent = c.Int(),
                    })
                .PrimaryKey(t => t.IDHero)
                .ForeignKey("dbo.Concepts", t => t.IDConcept)
                .ForeignKey("dbo.Contents", t => t.IDContent)
                .Index(t => t.IDConcept)
                .Index(t => t.IDContent);
            
            CreateTable(
                "dbo.Attributes",
                c => new
                    {
                        IDAttribute = c.Int(nullable: false, identity: true),
                        name = c.String(nullable: false),
                        level = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.IDAttribute);
            
            CreateTable(
                "dbo.Concepts",
                c => new
                    {
                        IDConcept = c.Int(nullable: false, identity: true),
                        level = c.Int(nullable: false),
                        description = c.String(),
                    })
                .PrimaryKey(t => t.IDConcept);
            
            CreateTable(
                "dbo.Monsters",
                c => new
                    {
                        IDMonster = c.Int(nullable: false, identity: true),
                        name = c.String(nullable: false),
                        description = c.String(nullable: false),
                        IDConcept = c.Int(),
                        IDContent = c.Int(),
                    })
                .PrimaryKey(t => t.IDMonster)
                .ForeignKey("dbo.Concepts", t => t.IDConcept)
                .ForeignKey("dbo.Contents", t => t.IDContent)
                .Index(t => t.IDConcept)
                .Index(t => t.IDContent);
            
            CreateTable(
                "dbo.Spells",
                c => new
                    {
                        IDSpell = c.Int(nullable: false, identity: true),
                        description = c.String(nullable: false),
                        IDConcept = c.Int(),
                        IDContent = c.Int(),
                        IDClass = c.Int(),
                    })
                .PrimaryKey(t => t.IDSpell)
                .ForeignKey("dbo.Classes", t => t.IDClass)
                .ForeignKey("dbo.Concepts", t => t.IDConcept)
                .ForeignKey("dbo.Contents", t => t.IDContent)
                .Index(t => t.IDConcept)
                .Index(t => t.IDContent)
                .Index(t => t.IDClass);
            
            CreateTable(
                "dbo.Equipments",
                c => new
                    {
                        IDEquipment = c.Int(nullable: false, identity: true),
                        IDHero = c.Int(),
                    })
                .PrimaryKey(t => t.IDEquipment)
                .ForeignKey("dbo.Heroes", t => t.IDHero)
                .Index(t => t.IDHero);
            
            CreateTable(
                "dbo.Labels",
                c => new
                    {
                        IDLabel = c.Int(nullable: false, identity: true),
                        name = c.String(nullable: false),
                        description = c.String(),
                        isActive = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.IDLabel);
            
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
                "dbo.AspNetRoles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");
            
            CreateTable(
                "dbo.CA",
                c => new
                    {
                        ArmorRefId = c.Int(nullable: false),
                        ClassRefId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.ArmorRefId, t.ClassRefId })
                .ForeignKey("dbo.Items", t => t.ArmorRefId, cascadeDelete: true)
                .ForeignKey("dbo.Classes", t => t.ClassRefId, cascadeDelete: true)
                .Index(t => t.ArmorRefId)
                .Index(t => t.ClassRefId);
            
            CreateTable(
                "dbo.HA",
                c => new
                    {
                        AttributeRefId = c.Int(nullable: false),
                        HeroRefId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.AttributeRefId, t.HeroRefId })
                .ForeignKey("dbo.Attributes", t => t.AttributeRefId, cascadeDelete: true)
                .ForeignKey("dbo.Heroes", t => t.HeroRefId, cascadeDelete: true)
                .Index(t => t.AttributeRefId)
                .Index(t => t.HeroRefId);
            
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
            
            CreateTable(
                "dbo.EI",
                c => new
                    {
                        ItemRefId = c.Int(nullable: false),
                        EquipmentRefId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.ItemRefId, t.EquipmentRefId })
                .ForeignKey("dbo.Items", t => t.ItemRefId, cascadeDelete: true)
                .ForeignKey("dbo.Equipments", t => t.EquipmentRefId, cascadeDelete: true)
                .Index(t => t.ItemRefId)
                .Index(t => t.EquipmentRefId);
            
            CreateTable(
                "dbo.CW",
                c => new
                    {
                        WeaponRefId = c.Int(nullable: false),
                        ClassRefId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.WeaponRefId, t.ClassRefId })
                .ForeignKey("dbo.Items", t => t.WeaponRefId, cascadeDelete: true)
                .ForeignKey("dbo.Classes", t => t.ClassRefId, cascadeDelete: true)
                .Index(t => t.WeaponRefId)
                .Index(t => t.ClassRefId);
            
            CreateTable(
                "dbo.HL",
                c => new
                    {
                        LabelRefId = c.Int(nullable: false),
                        HeroRefId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.LabelRefId, t.HeroRefId })
                .ForeignKey("dbo.Labels", t => t.LabelRefId, cascadeDelete: true)
                .ForeignKey("dbo.Heroes", t => t.HeroRefId, cascadeDelete: true)
                .Index(t => t.LabelRefId)
                .Index(t => t.HeroRefId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.Rules", "IDContent", "dbo.Contents");
            DropForeignKey("dbo.HL", "HeroRefId", "dbo.Heroes");
            DropForeignKey("dbo.HL", "LabelRefId", "dbo.Labels");
            DropForeignKey("dbo.CW", "ClassRefId", "dbo.Classes");
            DropForeignKey("dbo.CW", "WeaponRefId", "dbo.Items");
            DropForeignKey("dbo.EI", "EquipmentRefId", "dbo.Equipments");
            DropForeignKey("dbo.EI", "ItemRefId", "dbo.Items");
            DropForeignKey("dbo.Items", "IDContent", "dbo.Contents");
            DropForeignKey("dbo.Equipments", "IDHero", "dbo.Heroes");
            DropForeignKey("dbo.Heroes", "IDContent", "dbo.Contents");
            DropForeignKey("dbo.Spells", "IDContent", "dbo.Contents");
            DropForeignKey("dbo.Spells", "IDConcept", "dbo.Concepts");
            DropForeignKey("dbo.Spells", "IDClass", "dbo.Classes");
            DropForeignKey("dbo.Monsters", "IDContent", "dbo.Contents");
            DropForeignKey("dbo.Monsters", "IDConcept", "dbo.Concepts");
            DropForeignKey("dbo.Heroes", "IDConcept", "dbo.Concepts");
            DropForeignKey("dbo.CH", "ClassRefId", "dbo.Classes");
            DropForeignKey("dbo.CH", "HeroRefId", "dbo.Heroes");
            DropForeignKey("dbo.HA", "HeroRefId", "dbo.Heroes");
            DropForeignKey("dbo.HA", "AttributeRefId", "dbo.Attributes");
            DropForeignKey("dbo.Classes", "IDContent", "dbo.Contents");
            DropForeignKey("dbo.AspNetUsers", "Content_IDContent", "dbo.Contents");
            DropForeignKey("dbo.Contents", "ApplicationUserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Adventures", "IDContent", "dbo.Contents");
            DropForeignKey("dbo.CA", "ClassRefId", "dbo.Classes");
            DropForeignKey("dbo.CA", "ArmorRefId", "dbo.Items");
            DropForeignKey("dbo.Abilities", "IDClass", "dbo.Classes");
            DropIndex("dbo.HL", new[] { "HeroRefId" });
            DropIndex("dbo.HL", new[] { "LabelRefId" });
            DropIndex("dbo.CW", new[] { "ClassRefId" });
            DropIndex("dbo.CW", new[] { "WeaponRefId" });
            DropIndex("dbo.EI", new[] { "EquipmentRefId" });
            DropIndex("dbo.EI", new[] { "ItemRefId" });
            DropIndex("dbo.CH", new[] { "ClassRefId" });
            DropIndex("dbo.CH", new[] { "HeroRefId" });
            DropIndex("dbo.HA", new[] { "HeroRefId" });
            DropIndex("dbo.HA", new[] { "AttributeRefId" });
            DropIndex("dbo.CA", new[] { "ClassRefId" });
            DropIndex("dbo.CA", new[] { "ArmorRefId" });
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropIndex("dbo.Rules", new[] { "IDContent" });
            DropIndex("dbo.Equipments", new[] { "IDHero" });
            DropIndex("dbo.Spells", new[] { "IDClass" });
            DropIndex("dbo.Spells", new[] { "IDContent" });
            DropIndex("dbo.Spells", new[] { "IDConcept" });
            DropIndex("dbo.Monsters", new[] { "IDContent" });
            DropIndex("dbo.Monsters", new[] { "IDConcept" });
            DropIndex("dbo.Heroes", new[] { "IDContent" });
            DropIndex("dbo.Heroes", new[] { "IDConcept" });
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.AspNetUsers", new[] { "Content_IDContent" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropIndex("dbo.Adventures", new[] { "IDContent" });
            DropIndex("dbo.Contents", new[] { "ApplicationUserId" });
            DropIndex("dbo.Items", new[] { "IDContent" });
            DropIndex("dbo.Classes", new[] { "IDContent" });
            DropIndex("dbo.Abilities", new[] { "IDClass" });
            DropTable("dbo.HL");
            DropTable("dbo.CW");
            DropTable("dbo.EI");
            DropTable("dbo.CH");
            DropTable("dbo.HA");
            DropTable("dbo.CA");
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.Rules");
            DropTable("dbo.Labels");
            DropTable("dbo.Equipments");
            DropTable("dbo.Spells");
            DropTable("dbo.Monsters");
            DropTable("dbo.Concepts");
            DropTable("dbo.Attributes");
            DropTable("dbo.Heroes");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.Adventures");
            DropTable("dbo.Contents");
            DropTable("dbo.Items");
            DropTable("dbo.Classes");
            DropTable("dbo.Abilities");
        }
    }
}
