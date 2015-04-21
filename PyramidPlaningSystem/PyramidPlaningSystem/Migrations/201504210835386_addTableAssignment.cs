namespace PyramidPlaningSystem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addTableAssignment : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Assignments",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        AddedBy = c.String(),
                        TimeStamp = c.DateTime(nullable: false),
                        Todo_ToDoId = c.Guid(),
                        User_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ToDoes", t => t.Todo_ToDoId)
                .ForeignKey("dbo.AspNetUsers", t => t.User_Id)
                .Index(t => t.Todo_ToDoId)
                .Index(t => t.User_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Assignments", "User_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.Assignments", "Todo_ToDoId", "dbo.ToDoes");
            DropIndex("dbo.Assignments", new[] { "User_Id" });
            DropIndex("dbo.Assignments", new[] { "Todo_ToDoId" });
            DropTable("dbo.Assignments");
        }
    }
}
