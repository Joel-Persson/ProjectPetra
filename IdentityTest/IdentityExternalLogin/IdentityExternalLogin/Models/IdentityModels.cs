using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace IdentityExternalLogin.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit http://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
        [Required]
        [MaxLength(100)]
        public string Address { get; set; }

        [Required]
        [MaxLength(50)]
        public string City { get; set; }

        [Required]
        [MaxLength(50)]
        public string Firstname { get; set; }

        [Required]
        [MaxLength(50)]
        public string Lastname { get; set; }

        [Required]
        [MaxLength(50)]
        public string Phone { get; set; }

        [Required]
        [MaxLength(20)]
        public string ZipCode { get; set; }
        

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }
        
    
    }

    //public class CustomInitializer : DropCreateDatabaseIfModelChanges<ApplicationDbContext>
    //{
    //    protected override void Seed(ApplicationDbContext context)
    //    {
    //        var userManager = new UserManager<ApplicationUser>(new
    //            UserStore<ApplicationUser>(context));

    //        var roleManager = new RoleManager<IdentityRole>(new
    //            RoleStore<IdentityRole>(context));

    //        string name = "Admin";
    //        string password = "123456";


    //        //Create Role Admin if it does not exist
    //        if (!roleManager.RoleExists(name))
    //        {
    //            IdentityResult roleresult = roleManager.Create(new IdentityRole(name));
    //        }

    //        //Create User=Admin with password=123456
    //        var user = new ApplicationUser();
    //        user.UserName = name;
    //        IdentityResult adminresult = userManager.Create(user, password);

    //        //Add User Admin to Role Admin
    //        if (adminresult.Succeeded)
    //        {
    //            IdentityResult result = userManager.AddToRole(user.Id, name);
    //        }
    //        base.Seed(context);
    //    }
    //}

    public class ApplicationRole : IdentityRole
    {
        public ApplicationRole() : base() { }

        public ApplicationRole(string name) : base(name) { }
    }

}