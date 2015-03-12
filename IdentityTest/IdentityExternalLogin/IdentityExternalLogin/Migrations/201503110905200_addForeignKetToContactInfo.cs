namespace IdentityExternalLogin.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addForeignKetToContactInfo : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.AspNetUsers", "ContactInfo_Id", "dbo.ContactInfoes");
            DropIndex("dbo.AspNetUsers", new[] { "ContactInfo_Id" });
            RenameColumn(table: "dbo.AspNetUsers", name: "ContactInfo_Id", newName: "ContactInfoId");
            AlterColumn("dbo.AspNetUsers", "ContactInfoId", c => c.Int(nullable: false));
            CreateIndex("dbo.AspNetUsers", "ContactInfoId");
            AddForeignKey("dbo.AspNetUsers", "ContactInfoId", "dbo.ContactInfoes", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUsers", "ContactInfoId", "dbo.ContactInfoes");
            DropIndex("dbo.AspNetUsers", new[] { "ContactInfoId" });
            AlterColumn("dbo.AspNetUsers", "ContactInfoId", c => c.Int());
            RenameColumn(table: "dbo.AspNetUsers", name: "ContactInfoId", newName: "ContactInfo_Id");
            CreateIndex("dbo.AspNetUsers", "ContactInfo_Id");
            AddForeignKey("dbo.AspNetUsers", "ContactInfo_Id", "dbo.ContactInfoes", "Id");
        }
    }
}
