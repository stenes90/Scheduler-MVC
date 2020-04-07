namespace SchedulerV3.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ClassObjectPropUpdated : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Classes", "Name", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Classes", "Name", c => c.String());
        }
    }
}
