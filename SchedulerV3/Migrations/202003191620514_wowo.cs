namespace SchedulerV3.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class wowo : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Courts", "PlayingDates_Id", "dbo.PlayingDates");
            DropForeignKey("dbo.Courts", "Tournament_Id", "dbo.Tournaments");
            DropIndex("dbo.Courts", new[] { "PlayingDates_Id" });
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
                        PlayingDates_Id = c.Int(),
                        Tournament_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateIndex("dbo.Courts", "Tournament_Id");
            CreateIndex("dbo.Courts", "PlayingDates_Id");
            AddForeignKey("dbo.Courts", "Tournament_Id", "dbo.Tournaments", "Id");
            AddForeignKey("dbo.Courts", "PlayingDates_Id", "dbo.PlayingDates", "Id");
        }
    }
}
