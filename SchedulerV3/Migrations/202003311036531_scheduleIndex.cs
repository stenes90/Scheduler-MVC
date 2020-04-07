namespace SchedulerV3.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class scheduleIndex : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Matches", "matchScheduleIndex", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Matches", "matchScheduleIndex");
        }
    }
}
