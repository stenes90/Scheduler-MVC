namespace SchedulerV3.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Datetime : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.PlayingDates", "ClassId", "dbo.Classes");
            DropIndex("dbo.PlayingDates", new[] { "ClassId" });
            AddColumn("dbo.PlayingDates", "Date", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.PlayingDates", "Date");
            CreateIndex("dbo.PlayingDates", "ClassId");
            AddForeignKey("dbo.PlayingDates", "ClassId", "dbo.Classes", "Id", cascadeDelete: true);
        }
    }
}
