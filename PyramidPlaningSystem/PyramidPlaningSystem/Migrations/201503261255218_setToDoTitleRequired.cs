namespace PyramidPlaningSystem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class setToDoTitleRequired : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.ToDoes", "Title", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.ToDoes", "Title", c => c.String());
        }
    }
}
