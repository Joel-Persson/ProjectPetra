using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IdentityExternalLogin.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace IdentityExternalLogin.Infrastructure
{
    public class ApplicationDbInitializer : DropCreateDatabaseAlways<ApplicationDbContext>
    {

        private static readonly UserManager<ApplicationUser> usermanager =
            new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new ApplicationDbContext()));

        protected override void Seed(ApplicationDbContext context)
        {

            using (usermanager)
            {
                for (int i = 0; i < 10; i++)
                {
                    var email = "user" + (i*3) + "@example.com";
                    string phone = "12345678" + (i*100 + i);
                    var tempuser = new ApplicationUser { UserName = email, Email = email, ZipCode = "test"};
                    usermanager.Create(tempuser, "ASP+Rocks4U");
                }

                var appstore = new UserStore<ApplicationUser>();
                appstore.Context.SaveChanges();
                base.Seed(context);

            }
        }
    }
}