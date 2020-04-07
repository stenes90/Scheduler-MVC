namespace SchedulerV3.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class courtplayingdateforgeinkey : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Courts", "PlayingDateId", c => c.Int(nullable: false));
            CreateIndex("dbo.Courts", "PlayingDateId");
            AddForeignKey("dbo.Courts", "PlayingDateId", "dbo.PlayingDates", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Courts", "PlayingDateId", "dbo.PlayingDates");
            DropIndex("dbo.Courts", new[] { "PlayingDateId" });
            DropColumn("dbo.Courts", "PlayingDateId");
        }
    }
}
