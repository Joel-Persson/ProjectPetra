namespace PyramidPlaningSystem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addPriorityinToDo : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ToDoes", "Priority", c => c.Int());
        }
        
        public override void Down()
        {
            DropColumn("dbo.ToDoes", "Priority");
        }
    }
}
