namespace SchedulerV3.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ratkapna : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Ratkapnas",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Kola_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Kolas", t => t.Kola_Id)
                .Index(t => t.Kola_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Ratkapnas", "Kola_Id", "dbo.Kolas");
            DropIndex("dbo.Ratkapnas", new[] { "Kola_Id" });
            DropTable("dbo.Ratkapnas");
        }
    }
}
