namespace IdentityExternalLogin.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addContact : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ContactInfoes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Address = c.String(),
                        City = c.String(),
                        Firstname = c.String(),
                        Lastname = c.String(),
                        Phone = c.String(),
                        ZipCode = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.AspNetUsers", "ContactInfo_Id", c => c.Int());
            CreateIndex("dbo.AspNetUsers", "ContactInfo_Id");
            AddForeignKey("dbo.AspNetUsers", "ContactInfo_Id", "dbo.ContactInfoes", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUsers", "ContactInfo_Id", "dbo.ContactInfoes");
            DropIndex("dbo.AspNetUsers", new[] { "ContactInfo_Id" });
            DropColumn("dbo.AspNetUsers", "ContactInfo_Id");
            DropTable("dbo.ContactInfoes");
        }
    }
}
