namespace SchedulerV3.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ops : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Tournaments", "Class_Id", "dbo.Classes");
            DropForeignKey("dbo.Classes", "Tournament_Id", "dbo.Tournaments");
            DropIndex("dbo.Classes", new[] { "Tournament_Id" });
            DropIndex("dbo.Tournaments", new[] { "Class_Id" });
            DropColumn("dbo.Classes", "Tournament_Id");
            
            AlterColumn("dbo.Classes", "TournamentId", c => c.Int(nullable: false));
            CreateIndex("dbo.Classes", "TournamentId");
            AddForeignKey("dbo.Classes", "TournamentId", "dbo.Tournaments", "Id", cascadeDelete: true);
            DropColumn("dbo.Tournaments", "Class_Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Tournaments", "Class_Id", c => c.Int());
            DropForeignKey("dbo.Classes", "TournamentId", "dbo.Tournaments");
            DropIndex("dbo.Classes", new[] { "TournamentId" });
            AlterColumn("dbo.Classes", "TournamentId", c => c.Int());
            RenameColumn(table: "dbo.Classes", name: "TournamentId", newName: "Tournament_Id");
            AddColumn("dbo.Classes", "TournamentId", c => c.Int(nullable: false));
            CreateIndex("dbo.Tournaments", "Class_Id");
            CreateIndex("dbo.Classes", "Tournament_Id");
            AddForeignKey("dbo.Classes", "Tournament_Id", "dbo.Tournaments", "Id");
            AddForeignKey("dbo.Tournaments", "Class_Id", "dbo.Classes", "Id");
        }
    }
}
