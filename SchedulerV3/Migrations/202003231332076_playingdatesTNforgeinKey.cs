namespace SchedulerV3.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class playingdatesTNforgeinKey : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.PlayingDates", "Tournament_Id", c => c.Int());
            CreateIndex("dbo.PlayingDates", "Tournament_Id");
            AddForeignKey("dbo.PlayingDates", "Tournament_Id", "dbo.Tournaments", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.PlayingDates", "Tournament_Id", "dbo.Tournaments");
            DropIndex("dbo.PlayingDates", new[] { "Tournament_Id" });
            DropColumn("dbo.PlayingDates", "Tournament_Id");
        }
    }
}
