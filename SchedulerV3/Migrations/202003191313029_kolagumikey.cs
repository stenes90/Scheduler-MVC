namespace SchedulerV3.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class kolagumikey : DbMigration
    {
        public override void Up()
        {
            CreateIndex("dbo.Gumis", "KolaId");
            AddForeignKey("dbo.Gumis", "KolaId", "dbo.Kolas", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Gumis", "KolaId", "dbo.Kolas");
            DropIndex("dbo.Gumis", new[] { "KolaId" });
        }
    }
}
