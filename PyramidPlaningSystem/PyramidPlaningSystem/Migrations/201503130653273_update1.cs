namespace PyramidPlaningSystem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class update1 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Contacts",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Address = c.String(nullable: false, maxLength: 100),
                        City = c.String(nullable: false, maxLength: 50),
                        Firstname = c.String(nullable: false, maxLength: 50),
                        Lastname = c.String(nullable: false, maxLength: 50),
                        Phone = c.String(nullable: false, maxLength: 50),
                        ZipCode = c.String(nullable: false, maxLength: 20),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.AspNetUsers", "Contact_Id", c => c.String(maxLength: 128));
            CreateIndex("dbo.AspNetUsers", "Contact_Id");
            AddForeignKey("dbo.AspNetUsers", "Contact_Id", "dbo.Contacts", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUsers", "Contact_Id", "dbo.Contacts");
            DropIndex("dbo.AspNetUsers", new[] { "Contact_Id" });
            DropColumn("dbo.AspNetUsers", "Contact_Id");
            DropTable("dbo.Contacts");
        }
    }
}
