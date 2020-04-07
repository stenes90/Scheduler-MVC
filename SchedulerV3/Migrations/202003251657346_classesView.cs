namespace SchedulerV3.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class classesView : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Classes", "TournamentId", "dbo.Tournaments");
            DropIndex("dbo.Classes", new[] { "TournamentId" });
            AddColumn("dbo.Classes", "Tournament_Id", c => c.Int());
            AddColumn("dbo.Tournaments", "Class_Id", c => c.Int());
            CreateIndex("dbo.Classes", "Tournament_Id");
            CreateIndex("dbo.Tournaments", "Class_Id");
            AddForeignKey("dbo.Tournaments", "Class_Id", "dbo.Classes", "Id");
            AddForeignKey("dbo.Classes", "Tournament_Id", "dbo.Tournaments", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Classes", "Tournament_Id", "dbo.Tournaments");
            DropForeignKey("dbo.Tournaments", "Class_Id", "dbo.Classes");
            DropIndex("dbo.Tournaments", new[] { "Class_Id" });
            DropIndex("dbo.Classes", new[] { "Tournament_Id" });
            DropColumn("dbo.Tournaments", "Class_Id");
            DropColumn("dbo.Classes", "Tournament_Id");
            CreateIndex("dbo.Classes", "TournamentId");
            AddForeignKey("dbo.Classes", "TournamentId", "dbo.Tournaments", "Id", cascadeDelete: true);
        }
    }
}
