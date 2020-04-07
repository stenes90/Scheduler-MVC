namespace SchedulerV3.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class WTF : DbMigration
    {
        public override void Up()
        {
            CreateIndex("dbo.PlayingDates", "ClassId");
            AddForeignKey("dbo.PlayingDates", "ClassId", "dbo.Classes", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.PlayingDates", "ClassId", "dbo.Classes");
            DropIndex("dbo.PlayingDates", new[] { "ClassId" });
        }
    }
}
