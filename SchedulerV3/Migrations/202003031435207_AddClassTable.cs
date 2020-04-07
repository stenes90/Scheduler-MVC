namespace SchedulerV3.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddClassTable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Classes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        TournamentId = c.Int(nullable: false),
                        Name = c.String(),
                        NumberOfRounds = c.Int(nullable: false),
                        MatchesPerRound = c.Int(nullable: false),
                        MatchDuration = c.Int(nullable: false),
                        BreakBetweenMatches = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Classes");
        }
    }
}
