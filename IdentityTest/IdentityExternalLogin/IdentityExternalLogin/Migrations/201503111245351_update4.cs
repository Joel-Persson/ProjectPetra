namespace IdentityExternalLogin.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class update4 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.AspNetUsers", "ContactInfoId", "dbo.ContactInfoes");
            DropIndex("dbo.AspNetUsers", new[] { "ContactInfoId" });
            AddColumn("dbo.ContactInfoes", "ApplicationUser_Id", c => c.String(maxLength: 128));
            CreateIndex("dbo.ContactInfoes", "ApplicationUser_Id");
            AddForeignKey("dbo.ContactInfoes", "ApplicationUser_Id", "dbo.AspNetUsers", "Id");
            DropColumn("dbo.AspNetUsers", "ContactInfoId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.AspNetUsers", "ContactInfoId", c => c.Int(nullable: false));
            DropForeignKey("dbo.ContactInfoes", "ApplicationUser_Id", "dbo.AspNetUsers");
            DropIndex("dbo.ContactInfoes", new[] { "ApplicationUser_Id" });
            DropColumn("dbo.ContactInfoes", "ApplicationUser_Id");
            CreateIndex("dbo.AspNetUsers", "ContactInfoId");
            AddForeignKey("dbo.AspNetUsers", "ContactInfoId", "dbo.ContactInfoes", "Id", cascadeDelete: true);
        }
    }
}
