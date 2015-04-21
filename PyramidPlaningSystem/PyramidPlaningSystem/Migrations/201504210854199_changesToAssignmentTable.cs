namespace PyramidPlaningSystem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class changesToAssignmentTable : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Assignments", "Todo_ToDoId", "dbo.ToDoes");
            DropForeignKey("dbo.Assignments", "User_Id", "dbo.AspNetUsers");
            DropIndex("dbo.Assignments", new[] { "Todo_ToDoId" });
            DropIndex("dbo.Assignments", new[] { "User_Id" });
            AlterColumn("dbo.Assignments", "AddedBy", c => c.String(nullable: false));
            AlterColumn("dbo.Assignments", "Todo_ToDoId", c => c.Guid(nullable: false));
            AlterColumn("dbo.Assignments", "User_Id", c => c.String(nullable: false, maxLength: 128));
            CreateIndex("dbo.Assignments", "Todo_ToDoId");
            CreateIndex("dbo.Assignments", "User_Id");
            AddForeignKey("dbo.Assignments", "Todo_ToDoId", "dbo.ToDoes", "ToDoId", cascadeDelete: true);
            AddForeignKey("dbo.Assignments", "User_Id", "dbo.AspNetUsers", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Assignments", "User_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.Assignments", "Todo_ToDoId", "dbo.ToDoes");
            DropIndex("dbo.Assignments", new[] { "User_Id" });
            DropIndex("dbo.Assignments", new[] { "Todo_ToDoId" });
            AlterColumn("dbo.Assignments", "User_Id", c => c.String(maxLength: 128));
            AlterColumn("dbo.Assignments", "Todo_ToDoId", c => c.Guid());
            AlterColumn("dbo.Assignments", "AddedBy", c => c.String());
            CreateIndex("dbo.Assignments", "User_Id");
            CreateIndex("dbo.Assignments", "Todo_ToDoId");
            AddForeignKey("dbo.Assignments", "User_Id", "dbo.AspNetUsers", "Id");
            AddForeignKey("dbo.Assignments", "Todo_ToDoId", "dbo.ToDoes", "ToDoId");
        }
    }
}
