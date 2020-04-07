namespace SchedulerV3.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ListOfClassesAddedInPlayingDateModel : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.PlayingDates", "ClassId", "dbo.Classes");
            DropIndex("dbo.PlayingDates", new[] { "ClassId" });
            CreateTable(
                "dbo.PlayingDateClasses",
                c => new
                    {
                        PlayingDate_Id = c.Int(nullable: false),
                        Class_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.PlayingDate_Id, t.Class_Id })
                .ForeignKey("dbo.PlayingDates", t => t.PlayingDate_Id, cascadeDelete: true)
                .ForeignKey("dbo.Classes", t => t.Class_Id, cascadeDelete: true)
                .Index(t => t.PlayingDate_Id)
                .Index(t => t.Class_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.PlayingDateClasses", "Class_Id", "dbo.Classes");
            DropForeignKey("dbo.PlayingDateClasses", "PlayingDate_Id", "dbo.PlayingDates");
            DropIndex("dbo.PlayingDateClasses", new[] { "Class_Id" });
            DropIndex("dbo.PlayingDateClasses", new[] { "PlayingDate_Id" });
            DropTable("dbo.PlayingDateClasses");
            CreateIndex("dbo.PlayingDates", "ClassId");
            AddForeignKey("dbo.PlayingDates", "ClassId", "dbo.Classes", "Id", cascadeDelete: true);
        }
    }
}
