namespace PyramidPlaningSystem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class createdTableProjects : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Projects",
                c => new
                    {
                        Id = c.Guid(nullable: false, identity: true),
                        Projectname = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.ToDoes", "Project_Id", c => c.Guid());
            CreateIndex("dbo.ToDoes", "Project_Id");
            AddForeignKey("dbo.ToDoes", "Project_Id", "dbo.Projects", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ToDoes", "Project_Id", "dbo.Projects");
            DropIndex("dbo.ToDoes", new[] { "Project_Id" });
            DropColumn("dbo.ToDoes", "Project_Id");
            DropTable("dbo.Projects");
        }
    }
}
