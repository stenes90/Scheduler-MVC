namespace SchedulerV3.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MatchAddedInModels : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Matches",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Round = c.Int(nullable: false),
                        Date = c.DateTime(nullable: false),
                        StartTime = c.DateTime(nullable: false),
                        EndTime = c.DateTime(nullable: false),
                        Class_Id = c.Int(),
                        Court_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Classes", t => t.Class_Id)
                .ForeignKey("dbo.Courts", t => t.Court_Id)
                .Index(t => t.Class_Id)
                .Index(t => t.Court_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Matches", "Court_Id", "dbo.Courts");
            DropForeignKey("dbo.Matches", "Class_Id", "dbo.Classes");
            DropIndex("dbo.Matches", new[] { "Court_Id" });
            DropIndex("dbo.Matches", new[] { "Class_Id" });
            DropTable("dbo.Matches");
        }
    }
}
