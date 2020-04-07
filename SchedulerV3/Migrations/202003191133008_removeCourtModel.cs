namespace SchedulerV3.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class removeCourtModel : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Courts", "PlayingDate_Id", "dbo.PlayingDates");
            DropForeignKey("dbo.Courts", "PlayingDate_Id1", "dbo.PlayingDates");
            DropForeignKey("dbo.Courts", "Tournament_Id", "dbo.Tournaments");
            DropIndex("dbo.Courts", new[] { "PlayingDate_Id" });
            DropIndex("dbo.Courts", new[] { "PlayingDate_Id1" });
            DropIndex("dbo.Courts", new[] { "Tournament_Id" });
            DropTable("dbo.Courts");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.Courts",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        PlayingDate_Id = c.Int(),
                        PlayingDate_Id1 = c.Int(),
                        Tournament_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateIndex("dbo.Courts", "Tournament_Id");
            CreateIndex("dbo.Courts", "PlayingDate_Id1");
            CreateIndex("dbo.Courts", "PlayingDate_Id");
            AddForeignKey("dbo.Courts", "Tournament_Id", "dbo.Tournaments", "Id");
            AddForeignKey("dbo.Courts", "PlayingDate_Id1", "dbo.PlayingDates", "Id");
            AddForeignKey("dbo.Courts", "PlayingDate_Id", "dbo.PlayingDates", "Id");
        }
    }
}
