namespace PyramidPlaningSystem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addedFieldsDeletedStartedEnded : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ToDoes", "Deleted", c => c.Boolean(nullable: false));
            AddColumn("dbo.ToDoes", "Deadline", c => c.DateTime(nullable: false));
            AddColumn("dbo.ToDoes", "Started", c => c.DateTime());
            AddColumn("dbo.ToDoes", "Ended", c => c.DateTime());
        }
        
        public override void Down()
        {
            DropColumn("dbo.ToDoes", "Ended");
            DropColumn("dbo.ToDoes", "Started");
            DropColumn("dbo.ToDoes", "Deadline");
            DropColumn("dbo.ToDoes", "Deleted");
        }
    }
}
