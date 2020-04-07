namespace SchedulerV3.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PlayingDateMethodAdded : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Matches", "PlayingDate_Id", c => c.Int());
            CreateIndex("dbo.Matches", "PlayingDate_Id");
            AddForeignKey("dbo.Matches", "PlayingDate_Id", "dbo.PlayingDates", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Matches", "PlayingDate_Id", "dbo.PlayingDates");
            DropIndex("dbo.Matches", new[] { "PlayingDate_Id" });
            DropColumn("dbo.Matches", "PlayingDate_Id");
        }
    }
}
