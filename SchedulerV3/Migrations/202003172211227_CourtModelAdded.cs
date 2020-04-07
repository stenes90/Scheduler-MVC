namespace SchedulerV3.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CourtModelAdded : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Courts",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        PlayingDateId = c.Int(nullable: false),
                        PlayingDate_Id = c.Int(),
                        PlayingDate_Id1 = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.PlayingDates", t => t.PlayingDateId, cascadeDelete: true)
                .ForeignKey("dbo.PlayingDates", t => t.PlayingDate_Id)
                .ForeignKey("dbo.PlayingDates", t => t.PlayingDate_Id1)
                .Index(t => t.PlayingDateId)
                .Index(t => t.PlayingDate_Id)
                .Index(t => t.PlayingDate_Id1);
            
            AddColumn("dbo.PlayingDates", "StartTime", c => c.DateTime(nullable: false));
            AddColumn("dbo.PlayingDates", "EndTime", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Courts", "PlayingDate_Id1", "dbo.PlayingDates");
            DropForeignKey("dbo.Courts", "PlayingDate_Id", "dbo.PlayingDates");
            DropForeignKey("dbo.Courts", "PlayingDateId", "dbo.PlayingDates");
            DropIndex("dbo.Courts", new[] { "PlayingDate_Id1" });
            DropIndex("dbo.Courts", new[] { "PlayingDate_Id" });
            DropIndex("dbo.Courts", new[] { "PlayingDateId" });
            DropColumn("dbo.PlayingDates", "EndTime");
            DropColumn("dbo.PlayingDates", "StartTime");
            DropTable("dbo.Courts");
        }
    }
}
