namespace SchedulerV3.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class courtsadded : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Courts",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        PlayingDates_Id = c.Int(),
                        Tournament_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.PlayingDates", t => t.PlayingDates_Id)
                .ForeignKey("dbo.Tournaments", t => t.Tournament_Id)
                .Index(t => t.PlayingDates_Id)
                .Index(t => t.Tournament_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Courts", "Tournament_Id", "dbo.Tournaments");
            DropForeignKey("dbo.Courts", "PlayingDates_Id", "dbo.PlayingDates");
            DropIndex("dbo.Courts", new[] { "Tournament_Id" });
            DropIndex("dbo.Courts", new[] { "PlayingDates_Id" });
            DropTable("dbo.Courts");
        }
    }
}
