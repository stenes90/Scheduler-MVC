namespace SchedulerV3.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class strange : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Courts", "Tournament_Id", "dbo.Tournaments");
            AddColumn("dbo.Courts", "Tournament_Id1", c => c.Int());
            AddColumn("dbo.Tournaments", "Court_Id", c => c.Int());
            CreateIndex("dbo.Courts", "Tournament_Id1");
            CreateIndex("dbo.Tournaments", "Court_Id");
            AddForeignKey("dbo.Tournaments", "Court_Id", "dbo.Courts", "Id");
            AddForeignKey("dbo.Courts", "Tournament_Id1", "dbo.Tournaments", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Courts", "Tournament_Id1", "dbo.Tournaments");
            DropForeignKey("dbo.Tournaments", "Court_Id", "dbo.Courts");
            DropIndex("dbo.Tournaments", new[] { "Court_Id" });
            DropIndex("dbo.Courts", new[] { "Tournament_Id1" });
            DropColumn("dbo.Tournaments", "Court_Id");
            DropColumn("dbo.Courts", "Tournament_Id1");
            AddForeignKey("dbo.Courts", "Tournament_Id", "dbo.Tournaments", "Id");
        }
    }
}
