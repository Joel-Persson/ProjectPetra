namespace PyramidPlaningSystem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class changeNameOnStartAndEndDate : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ToDoes", "StartDate", c => c.DateTime());
            AddColumn("dbo.ToDoes", "EndDate", c => c.DateTime());
            DropColumn("dbo.ToDoes", "Started");
            DropColumn("dbo.ToDoes", "Ended");
        }
        
        public override void Down()
        {
            AddColumn("dbo.ToDoes", "Ended", c => c.DateTime());
            AddColumn("dbo.ToDoes", "Started", c => c.DateTime());
            DropColumn("dbo.ToDoes", "EndDate");
            DropColumn("dbo.ToDoes", "StartDate");
        }
    }
}
