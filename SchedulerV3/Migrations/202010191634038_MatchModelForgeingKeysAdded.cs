namespace SchedulerV3.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MatchModelForgeingKeysAdded : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Matches", "PlayingDate_Id", "dbo.PlayingDates");
            DropForeignKey("dbo.Matches", "Court_Id", "dbo.Courts");
            DropForeignKey("dbo.Matches", "Class_Id", "dbo.Classes");
            DropIndex("dbo.Matches", new[] { "Class_Id" });
            DropIndex("dbo.Matches", new[] { "Court_Id" });
            DropIndex("dbo.Matches", new[] { "PlayingDate_Id" });
            RenameColumn(table: "dbo.Matches", name: "PlayingDate_Id", newName: "PlayingDateId");
            RenameColumn(table: "dbo.Matches", name: "Court_Id", newName: "CourtId");
            RenameColumn(table: "dbo.Matches", name: "Class_Id", newName: "ClassId");
            RenameColumn(table: "dbo.Matches", name: "Tournament_Id", newName: "TournamentId");
            RenameIndex(table: "dbo.Matches", name: "IX_Tournament_Id", newName: "IX_TournamentId");
            AlterColumn("dbo.Matches", "ClassId", c => c.Int(nullable: false));
            AlterColumn("dbo.Matches", "CourtId", c => c.Int(nullable: false));
            AlterColumn("dbo.Matches", "PlayingDateId", c => c.Int(nullable: false));
            CreateIndex("dbo.Matches", "CourtId");
            CreateIndex("dbo.Matches", "ClassId");
            CreateIndex("dbo.Matches", "PlayingDateId");
            AddForeignKey("dbo.Matches", "PlayingDateId", "dbo.PlayingDates", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Matches", "CourtId", "dbo.Courts", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Matches", "ClassId", "dbo.Classes", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Matches", "ClassId", "dbo.Classes");
            DropForeignKey("dbo.Matches", "CourtId", "dbo.Courts");
            DropForeignKey("dbo.Matches", "PlayingDateId", "dbo.PlayingDates");
            DropIndex("dbo.Matches", new[] { "PlayingDateId" });
            DropIndex("dbo.Matches", new[] { "ClassId" });
            DropIndex("dbo.Matches", new[] { "CourtId" });
            AlterColumn("dbo.Matches", "PlayingDateId", c => c.Int());
            AlterColumn("dbo.Matches", "CourtId", c => c.Int());
            AlterColumn("dbo.Matches", "ClassId", c => c.Int());
            RenameIndex(table: "dbo.Matches", name: "IX_TournamentId", newName: "IX_Tournament_Id");
            RenameColumn(table: "dbo.Matches", name: "TournamentId", newName: "Tournament_Id");
            RenameColumn(table: "dbo.Matches", name: "ClassId", newName: "Class_Id");
            RenameColumn(table: "dbo.Matches", name: "CourtId", newName: "Court_Id");
            RenameColumn(table: "dbo.Matches", name: "PlayingDateId", newName: "PlayingDate_Id");
            CreateIndex("dbo.Matches", "PlayingDate_Id");
            CreateIndex("dbo.Matches", "Court_Id");
            CreateIndex("dbo.Matches", "Class_Id");
            AddForeignKey("dbo.Matches", "Class_Id", "dbo.Classes", "Id");
            AddForeignKey("dbo.Matches", "Court_Id", "dbo.Courts", "Id");
            AddForeignKey("dbo.Matches", "PlayingDate_Id", "dbo.PlayingDates", "Id");
        }
    }
}
