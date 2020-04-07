namespace SchedulerV3.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class bandaziratkapni : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Ratkapnas", "Bandazi_Id", c => c.Int());
            CreateIndex("dbo.Ratkapnas", "Bandazi_Id");
            AddForeignKey("dbo.Ratkapnas", "Bandazi_Id", "dbo.Bandazis", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Ratkapnas", "Bandazi_Id", "dbo.Bandazis");
            DropIndex("dbo.Ratkapnas", new[] { "Bandazi_Id" });
            DropColumn("dbo.Ratkapnas", "Bandazi_Id");
        }
    }
}
