namespace SchedulerV3.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class deleted_court : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Courts", "PlayingDate_Id", "dbo.PlayingDates");
            DropIndex("dbo.Courts", new[] { "PlayingDate_Id" });
            DropTable("dbo.Courts");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.Courts",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        TournamentId = c.Int(nullable: false),
                        Name = c.String(),
                        PlayingDate_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateIndex("dbo.Courts", "PlayingDate_Id");
            AddForeignKey("dbo.Courts", "PlayingDate_Id", "dbo.PlayingDates", "Id");
        }
    }
}
