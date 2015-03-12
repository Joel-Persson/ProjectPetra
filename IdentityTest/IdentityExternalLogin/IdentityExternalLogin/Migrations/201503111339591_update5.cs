namespace IdentityExternalLogin.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class update5 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.ContactInfoes", "ApplicationUser_Id", "dbo.AspNetUsers");
            DropIndex("dbo.ContactInfoes", new[] { "ApplicationUser_Id" });
            AlterColumn("dbo.ContactInfoes", "ApplicationUser_Id", c => c.String(nullable: false, maxLength: 128));
            CreateIndex("dbo.ContactInfoes", "ApplicationUser_Id");
            AddForeignKey("dbo.ContactInfoes", "ApplicationUser_Id", "dbo.AspNetUsers", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ContactInfoes", "ApplicationUser_Id", "dbo.AspNetUsers");
            DropIndex("dbo.ContactInfoes", new[] { "ApplicationUser_Id" });
            AlterColumn("dbo.ContactInfoes", "ApplicationUser_Id", c => c.String(maxLength: 128));
            CreateIndex("dbo.ContactInfoes", "ApplicationUser_Id");
            AddForeignKey("dbo.ContactInfoes", "ApplicationUser_Id", "dbo.AspNetUsers", "Id");
        }
    }
}
