namespace SchedulerV3.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class startEndTimeNullableInPlayingDatesModel : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.PlayingDates", "StartTime", c => c.DateTime());
            AlterColumn("dbo.PlayingDates", "EndTime", c => c.DateTime());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.PlayingDates", "EndTime", c => c.DateTime(nullable: false));
            AlterColumn("dbo.PlayingDates", "StartTime", c => c.DateTime(nullable: false));
        }
    }
}
