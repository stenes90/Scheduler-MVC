namespace SchedulerV3.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class carPartsRemovedFromModels : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Bandazis", "Kola_Id", "dbo.Kolas");
            DropForeignKey("dbo.Gumis", "KolaId", "dbo.Kolas");
            DropForeignKey("dbo.Ratkapnas", "Bandazi_Id", "dbo.Bandazis");
            DropForeignKey("dbo.Ratkapnas", "Kola_Id", "dbo.Kolas");
            DropIndex("dbo.Bandazis", new[] { "Kola_Id" });
            DropIndex("dbo.Gumis", new[] { "KolaId" });
            DropIndex("dbo.Ratkapnas", new[] { "Bandazi_Id" });
            DropIndex("dbo.Ratkapnas", new[] { "Kola_Id" });
            DropTable("dbo.Bandazis");
            DropTable("dbo.Kolas");
            DropTable("dbo.Gumis");
            DropTable("dbo.Ratkapnas");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.Ratkapnas",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Bandazi_Id = c.Int(),
                        Kola_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Gumis",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        KolaId = c.Int(nullable: false),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Kolas",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Bandazis",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Kola_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateIndex("dbo.Ratkapnas", "Kola_Id");
            CreateIndex("dbo.Ratkapnas", "Bandazi_Id");
            CreateIndex("dbo.Gumis", "KolaId");
            CreateIndex("dbo.Bandazis", "Kola_Id");
            AddForeignKey("dbo.Ratkapnas", "Kola_Id", "dbo.Kolas", "Id");
            AddForeignKey("dbo.Ratkapnas", "Bandazi_Id", "dbo.Bandazis", "Id");
            AddForeignKey("dbo.Gumis", "KolaId", "dbo.Kolas", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Bandazis", "Kola_Id", "dbo.Kolas", "Id");
        }
    }
}
