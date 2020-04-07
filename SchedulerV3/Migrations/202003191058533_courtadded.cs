namespace SchedulerV3.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class courtadded : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Courts", "Name", c => c.String());
            AddColumn("dbo.Courts", "Tournament_Id", c => c.Int());
            CreateIndex("dbo.Courts", "Tournament_Id");
            AddForeignKey("dbo.Courts", "Tournament_Id", "dbo.Tournaments", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Courts", "Tournament_Id", "dbo.Tournaments");
            DropIndex("dbo.Courts", new[] { "Tournament_Id" });
            DropColumn("dbo.Courts", "Tournament_Id");
            DropColumn("dbo.Courts", "Name");
        }
    }
}
