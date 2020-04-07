namespace SchedulerV3.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class removeClassIdfromplayingdatesobject : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.PlayingDates", "ClassId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.PlayingDates", "ClassId", c => c.Int(nullable: false));
        }
    }
}
