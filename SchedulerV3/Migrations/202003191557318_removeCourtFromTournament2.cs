namespace SchedulerV3.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class removeCourtFromTournament2 : DbMigration
    {
        public override void Up()
        {
            Sql("DROP INDEX [IX_Tournament_Id] ON [Courts];");
        }
        
        public override void Down()
        {
            
        }
    }
}
