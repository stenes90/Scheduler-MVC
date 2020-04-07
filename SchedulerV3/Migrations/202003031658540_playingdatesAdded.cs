namespace SchedulerV3.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class playingdatesAdded : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.PlayingDates", "ClassId", c => c.Int(nullable: false));
            AddColumn("dbo.PlayingDates", "TournamentId", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.PlayingDates", "TournamentId");
            DropColumn("dbo.PlayingDates", "ClassId");
        }
    }
}
