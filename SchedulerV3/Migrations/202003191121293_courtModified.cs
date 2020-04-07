namespace SchedulerV3.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class courtModified : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Courts", "PlayingDateId", "dbo.PlayingDates");
            DropIndex("dbo.Courts", new[] { "PlayingDateId" });
            DropColumn("dbo.Courts", "PlayingDateId");
            DropColumn("dbo.Courts", "Name");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Courts", "Name", c => c.String());
            AddColumn("dbo.Courts", "PlayingDateId", c => c.Int(nullable: false));
            CreateIndex("dbo.Courts", "PlayingDateId");
            AddForeignKey("dbo.Courts", "PlayingDateId", "dbo.PlayingDates", "Id", cascadeDelete: true);
        }
    }
}
