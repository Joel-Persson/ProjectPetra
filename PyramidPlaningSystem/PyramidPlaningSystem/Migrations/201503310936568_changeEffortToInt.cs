namespace PyramidPlaningSystem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class changeEffortToInt : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.ToDoes", "Effort", c => c.Int());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.ToDoes", "Effort", c => c.Time(precision: 7));
        }
    }
}
