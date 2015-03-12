namespace IdentityExternalLogin.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class dataNotations : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.ContactInfoes", "Address", c => c.String(nullable: false, maxLength: 100));
            AlterColumn("dbo.ContactInfoes", "City", c => c.String(nullable: false, maxLength: 50));
            AlterColumn("dbo.ContactInfoes", "Firstname", c => c.String(nullable: false, maxLength: 50));
            AlterColumn("dbo.ContactInfoes", "Lastname", c => c.String(nullable: false, maxLength: 50));
            AlterColumn("dbo.ContactInfoes", "Phone", c => c.String(nullable: false, maxLength: 50));
            AlterColumn("dbo.ContactInfoes", "ZipCode", c => c.String(nullable: false, maxLength: 20));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.ContactInfoes", "ZipCode", c => c.String());
            AlterColumn("dbo.ContactInfoes", "Phone", c => c.String());
            AlterColumn("dbo.ContactInfoes", "Lastname", c => c.String());
            AlterColumn("dbo.ContactInfoes", "Firstname", c => c.String());
            AlterColumn("dbo.ContactInfoes", "City", c => c.String());
            AlterColumn("dbo.ContactInfoes", "Address", c => c.String());
        }
    }
}
