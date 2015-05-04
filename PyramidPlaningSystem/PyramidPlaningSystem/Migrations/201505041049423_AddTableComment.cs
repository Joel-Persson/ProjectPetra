namespace PyramidPlaningSystem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddTableComment : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Comments",
                c => new
                    {
                        CommentId = c.Guid(nullable: false, identity: true),
                        Author = c.String(nullable: false),
                        Content = c.String(nullable: false),
                        Date = c.DateTime(nullable: false),
                        ToDo_ToDoId = c.Guid(),
                    })
                .PrimaryKey(t => t.CommentId)
                .ForeignKey("dbo.ToDoes", t => t.ToDo_ToDoId)
                .Index(t => t.ToDo_ToDoId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Comments", "ToDo_ToDoId", "dbo.ToDoes");
            DropIndex("dbo.Comments", new[] { "ToDo_ToDoId" });
            DropTable("dbo.Comments");
        }
    }
}
