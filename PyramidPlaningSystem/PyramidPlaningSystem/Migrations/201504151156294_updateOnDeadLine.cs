namespace PyramidPlaningSystem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updateOnDeadLine : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.ToDoes", "Deadline", c => c.DateTime());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.ToDoes", "Deadline", c => c.DateTime(nullable: false));
        }
    }
}
