namespace SchedulerV3.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class courtTNforgeinkey : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Courts", "TournamentId", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Courts", "TournamentId");
        }
    }
}
