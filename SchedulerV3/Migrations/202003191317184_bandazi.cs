namespace SchedulerV3.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class bandazi : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Bandazis",
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
            DropForeignKey("dbo.Bandazis", "Kola_Id", "dbo.Kolas");
            DropIndex("dbo.Bandazis", new[] { "Kola_Id" });
            DropTable("dbo.Bandazis");
        }
    }
}
