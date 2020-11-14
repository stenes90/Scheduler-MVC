namespace SchedulerV3.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class testMatchModel3 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Match_2", "Date", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Match_2", "Date");
        }
    }
}
