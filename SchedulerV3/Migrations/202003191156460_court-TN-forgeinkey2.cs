namespace SchedulerV3.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class courtTNforgeinkey2 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Courts", "PlayingDateId", "dbo.PlayingDates");
            DropIndex("dbo.Courts", new[] { "PlayingDateId" });
            RenameColumn(table: "dbo.Courts", name: "PlayingDateId", newName: "PlayingDate_Id");
            AlterColumn("dbo.Courts", "PlayingDate_Id", c => c.Int());
            CreateIndex("dbo.Courts", "PlayingDate_Id");
            AddForeignKey("dbo.Courts", "PlayingDate_Id", "dbo.PlayingDates", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Courts", "PlayingDate_Id", "dbo.PlayingDates");
            DropIndex("dbo.Courts", new[] { "PlayingDate_Id" });
            AlterColumn("dbo.Courts", "PlayingDate_Id", c => c.Int(nullable: false));
            RenameColumn(table: "dbo.Courts", name: "PlayingDate_Id", newName: "PlayingDateId");
            CreateIndex("dbo.Courts", "PlayingDateId");
            AddForeignKey("dbo.Courts", "PlayingDateId", "dbo.PlayingDates", "Id", cascadeDelete: true);
        }
    }
}
