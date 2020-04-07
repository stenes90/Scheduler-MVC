namespace SchedulerV3.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class courtPlayingDatesMadeList : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Courts", "PlayingDates_Id", "dbo.PlayingDates");
            DropIndex("dbo.Courts", new[] { "PlayingDates_Id" });
            CreateTable(
                "dbo.CourtPlayingDates",
                c => new
                    {
                        Court_Id = c.Int(nullable: false),
                        PlayingDate_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Court_Id, t.PlayingDate_Id })
                .ForeignKey("dbo.Courts", t => t.Court_Id, cascadeDelete: true)
                .ForeignKey("dbo.PlayingDates", t => t.PlayingDate_Id, cascadeDelete: true)
                .Index(t => t.Court_Id)
                .Index(t => t.PlayingDate_Id);
            
            DropColumn("dbo.Courts", "PlayingDates_Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Courts", "PlayingDates_Id", c => c.Int());
            DropForeignKey("dbo.CourtPlayingDates", "PlayingDate_Id", "dbo.PlayingDates");
            DropForeignKey("dbo.CourtPlayingDates", "Court_Id", "dbo.Courts");
            DropIndex("dbo.CourtPlayingDates", new[] { "PlayingDate_Id" });
            DropIndex("dbo.CourtPlayingDates", new[] { "Court_Id" });
            DropTable("dbo.CourtPlayingDates");
            CreateIndex("dbo.Courts", "PlayingDates_Id");
            AddForeignKey("dbo.Courts", "PlayingDates_Id", "dbo.PlayingDates", "Id");
        }
    }
}
