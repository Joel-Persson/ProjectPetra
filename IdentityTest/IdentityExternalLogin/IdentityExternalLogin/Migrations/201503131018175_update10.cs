namespace IdentityExternalLogin.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class update10 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.AspNetUsers", "ZipCode", c => c.String());
            DropColumn("dbo.AspNetUsers", "Address");
            DropColumn("dbo.AspNetUsers", "City");
            DropColumn("dbo.AspNetUsers", "Firstname");
            DropColumn("dbo.AspNetUsers", "Lastname");
            DropColumn("dbo.AspNetUsers", "Phone");
        }
        
        public override void Down()
        {
            AddColumn("dbo.AspNetUsers", "Phone", c => c.String(nullable: false, maxLength: 50));
            AddColumn("dbo.AspNetUsers", "Lastname", c => c.String(nullable: false, maxLength: 50));
            AddColumn("dbo.AspNetUsers", "Firstname", c => c.String(nullable: false, maxLength: 50));
            AddColumn("dbo.AspNetUsers", "City", c => c.String(nullable: false, maxLength: 50));
            AddColumn("dbo.AspNetUsers", "Address", c => c.String(nullable: false, maxLength: 100));
            AlterColumn("dbo.AspNetUsers", "ZipCode", c => c.String(nullable: false, maxLength: 20));
        }
    }
}
