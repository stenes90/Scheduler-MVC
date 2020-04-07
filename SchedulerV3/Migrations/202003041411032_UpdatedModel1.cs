namespace SchedulerV3.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdatedModel1 : DbMigration
    {
        public override void Up()
        {
            CreateIndex("dbo.PlayingDates", "ClassId");
            AddForeignKey("dbo.PlayingDates", "ClassId", "dbo.Classes", "Id", cascadeDelete: true);
            DropColumn("dbo.PlayingDates", "TournamentId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.PlayingDates", "TournamentId", c => c.Int(nullable: false));
            DropForeignKey("dbo.PlayingDates", "ClassId", "dbo.Classes");
            DropIndex("dbo.PlayingDates", new[] { "ClassId" });
        }
    }
}
