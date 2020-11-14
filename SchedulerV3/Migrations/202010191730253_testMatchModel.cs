namespace SchedulerV3.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class testMatchModel : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Match_2",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        TournamentId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Tournaments", t => t.TournamentId, cascadeDelete: true)
                .Index(t => t.TournamentId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Match_2", "TournamentId", "dbo.Tournaments");
            DropIndex("dbo.Match_2", new[] { "TournamentId" });
            DropTable("dbo.Match_2");
        }
    }
}
