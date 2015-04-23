namespace PyramidPlaningSystem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class identityOnAssignmentID : DbMigration
    {
        public override void Up()
        {
            DropPrimaryKey("dbo.Assignments");
            AlterColumn("dbo.Assignments", "Id", c => c.Guid(nullable: false, identity: true));
            AddPrimaryKey("dbo.Assignments", "Id");
        }
        
        public override void Down()
        {
            DropPrimaryKey("dbo.Assignments");
            AlterColumn("dbo.Assignments", "Id", c => c.Guid(nullable: false));
            AddPrimaryKey("dbo.Assignments", "Id");
        }
    }
}
