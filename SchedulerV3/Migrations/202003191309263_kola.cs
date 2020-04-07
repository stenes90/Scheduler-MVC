namespace SchedulerV3.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class kola : DbMigration
    {
        public override void Up()
        {
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
            
            DropColumn("dbo.Courts", "TournamentId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Courts", "TournamentId", c => c.Int(nullable: false));
            DropTable("dbo.Kolas");
            DropTable("dbo.Gumis");
        }
    }
}
