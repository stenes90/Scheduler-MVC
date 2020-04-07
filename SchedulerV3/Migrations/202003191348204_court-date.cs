namespace SchedulerV3.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class courtdate : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.CourtPlayingDates", "Court_Id", "dbo.Courts");
            DropForeignKey("dbo.CourtPlayingDates", "PlayingDate_Id", "dbo.PlayingDates");
            DropIndex("dbo.CourtPlayingDates", new[] { "Court_Id" });
            DropIndex("dbo.CourtPlayingDates", new[] { "PlayingDate_Id" });
            AddColumn("dbo.Courts", "PlayingDates_Id", c => c.Int());
            CreateIndex("dbo.Courts", "PlayingDates_Id");
            AddForeignKey("dbo.Courts", "PlayingDates_Id", "dbo.PlayingDates", "Id");
            DropTable("dbo.CourtPlayingDates");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.CourtPlayingDates",
                c => new
                    {
                        Court_Id = c.Int(nullable: false),
                        PlayingDate_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Court_Id, t.PlayingDate_Id });
            
            DropForeignKey("dbo.Courts", "PlayingDates_Id", "dbo.PlayingDates");
            DropIndex("dbo.Courts", new[] { "PlayingDates_Id" });
            DropColumn("dbo.Courts", "PlayingDates_Id");
            CreateIndex("dbo.CourtPlayingDates", "PlayingDate_Id");
            CreateIndex("dbo.CourtPlayingDates", "Court_Id");
            AddForeignKey("dbo.CourtPlayingDates", "PlayingDate_Id", "dbo.PlayingDates", "Id", cascadeDelete: true);
            AddForeignKey("dbo.CourtPlayingDates", "Court_Id", "dbo.Courts", "Id", cascadeDelete: true);
        }
    }
}
