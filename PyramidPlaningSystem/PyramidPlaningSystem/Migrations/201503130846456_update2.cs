namespace PyramidPlaningSystem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class update2 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.AspNetUsers", "Contact_Id", "dbo.Contacts");
            DropIndex("dbo.AspNetUsers", new[] { "Contact_Id" });
            DropPrimaryKey("dbo.Contacts");
            AlterColumn("dbo.Contacts", "Id", c => c.Int(nullable: false, identity: true));
            AlterColumn("dbo.AspNetUsers", "Contact_Id", c => c.Int());
            AddPrimaryKey("dbo.Contacts", "Id");
            CreateIndex("dbo.AspNetUsers", "Contact_Id");
            AddForeignKey("dbo.AspNetUsers", "Contact_Id", "dbo.Contacts", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUsers", "Contact_Id", "dbo.Contacts");
            DropIndex("dbo.AspNetUsers", new[] { "Contact_Id" });
            DropPrimaryKey("dbo.Contacts");
            AlterColumn("dbo.AspNetUsers", "Contact_Id", c => c.String(maxLength: 128));
            AlterColumn("dbo.Contacts", "Id", c => c.String(nullable: false, maxLength: 128));
            AddPrimaryKey("dbo.Contacts", "Id");
            CreateIndex("dbo.AspNetUsers", "Contact_Id");
            AddForeignKey("dbo.AspNetUsers", "Contact_Id", "dbo.Contacts", "Id");
        }
    }
}
