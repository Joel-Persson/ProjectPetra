namespace IdentityExternalLogin.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class update6 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.ContactInfoes", "ApplicationUser_Id", "dbo.AspNetUsers");
            DropIndex("dbo.ContactInfoes", new[] { "ApplicationUser_Id" });
            AddColumn("dbo.AspNetUsers", "Address", c => c.String(nullable: false, maxLength: 100));
            AddColumn("dbo.AspNetUsers", "City", c => c.String(nullable: false, maxLength: 50));
            AddColumn("dbo.AspNetUsers", "Firstname", c => c.String(nullable: false, maxLength: 50));
            AddColumn("dbo.AspNetUsers", "Lastname", c => c.String(nullable: false, maxLength: 50));
            AddColumn("dbo.AspNetUsers", "Phone", c => c.String(nullable: false, maxLength: 50));
            AddColumn("dbo.AspNetUsers", "ZipCode", c => c.String(nullable: false, maxLength: 20));
            DropTable("dbo.ContactInfoes");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.ContactInfoes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Address = c.String(nullable: false, maxLength: 100),
                        City = c.String(nullable: false, maxLength: 50),
                        Firstname = c.String(nullable: false, maxLength: 50),
                        Lastname = c.String(nullable: false, maxLength: 50),
                        Phone = c.String(nullable: false, maxLength: 50),
                        ZipCode = c.String(nullable: false, maxLength: 20),
                        ApplicationUser_Id = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.Id);
            
            DropColumn("dbo.AspNetUsers", "ZipCode");
            DropColumn("dbo.AspNetUsers", "Phone");
            DropColumn("dbo.AspNetUsers", "Lastname");
            DropColumn("dbo.AspNetUsers", "Firstname");
            DropColumn("dbo.AspNetUsers", "City");
            DropColumn("dbo.AspNetUsers", "Address");
            CreateIndex("dbo.ContactInfoes", "ApplicationUser_Id");
            AddForeignKey("dbo.ContactInfoes", "ApplicationUser_Id", "dbo.AspNetUsers", "Id", cascadeDelete: true);
        }
    }
}
