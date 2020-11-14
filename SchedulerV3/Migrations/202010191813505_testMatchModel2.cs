namespace SchedulerV3.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class testMatchModel2 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Match_2", "TournamentId", "dbo.Tournaments");
            DropIndex("dbo.Match_2", new[] { "TournamentId" });
        }
        
        public override void Down()
        {
            CreateIndex("dbo.Match_2", "TournamentId");
            AddForeignKey("dbo.Match_2", "TournamentId", "dbo.Tournaments", "Id", cascadeDelete: true);
        }
    }
}
