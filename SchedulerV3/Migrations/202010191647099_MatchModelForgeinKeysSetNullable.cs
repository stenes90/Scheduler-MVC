namespace SchedulerV3.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MatchModelForgeinKeysSetNullable : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Matches", "PlayingDateId", "dbo.PlayingDates");
            DropForeignKey("dbo.Matches", "CourtId", "dbo.Courts");
            DropForeignKey("dbo.Matches", "ClassId", "dbo.Classes");
            DropIndex("dbo.Matches", new[] { "CourtId" });
            DropIndex("dbo.Matches", new[] { "ClassId" });
            DropIndex("dbo.Matches", new[] { "PlayingDateId" });
            AlterColumn("dbo.Matches", "CourtId", c => c.Int());
            AlterColumn("dbo.Matches", "ClassId", c => c.Int());
            AlterColumn("dbo.Matches", "PlayingDateId", c => c.Int());
            CreateIndex("dbo.Matches", "CourtId");
            CreateIndex("dbo.Matches", "ClassId");
            CreateIndex("dbo.Matches", "PlayingDateId");
            AddForeignKey("dbo.Matches", "PlayingDateId", "dbo.PlayingDates", "Id");
            AddForeignKey("dbo.Matches", "CourtId", "dbo.Courts", "Id");
            AddForeignKey("dbo.Matches", "ClassId", "dbo.Classes", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Matches", "ClassId", "dbo.Classes");
            DropForeignKey("dbo.Matches", "CourtId", "dbo.Courts");
            DropForeignKey("dbo.Matches", "PlayingDateId", "dbo.PlayingDates");
            DropIndex("dbo.Matches", new[] { "PlayingDateId" });
            DropIndex("dbo.Matches", new[] { "ClassId" });
            DropIndex("dbo.Matches", new[] { "CourtId" });
            AlterColumn("dbo.Matches", "PlayingDateId", c => c.Int(nullable: false));
            AlterColumn("dbo.Matches", "ClassId", c => c.Int(nullable: false));
            AlterColumn("dbo.Matches", "CourtId", c => c.Int(nullable: false));
            CreateIndex("dbo.Matches", "PlayingDateId");
            CreateIndex("dbo.Matches", "ClassId");
            CreateIndex("dbo.Matches", "CourtId");
            AddForeignKey("dbo.Matches", "ClassId", "dbo.Classes", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Matches", "CourtId", "dbo.Courts", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Matches", "PlayingDateId", "dbo.PlayingDates", "Id", cascadeDelete: true);
        }
    }
}
