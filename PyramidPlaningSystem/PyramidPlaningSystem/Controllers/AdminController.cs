using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using PyramidPlaningSystem.Authorize;
using PyramidPlaningSystem.Models;

namespace PyramidPlaningSystem.Controllers
{
    [CustomAuthorize(Roles = "Administrator")]
    public class AdminController : Controller
    {
        // GET: Admin

        private ApplicationUserManager UserManager
        {
            get { return HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>(); }
        }

        private ApplicationRoleManager RoleManager
        {
            get { return HttpContext.GetOwinContext().GetUserManager<ApplicationRoleManager>(); }
        }
        public ActionResult Index()
        {
            return View(UserManager.Users);
        }

        [HttpPost]
        public async Task<ActionResult> Delete(string id)
        {
            ApplicationUser user = await UserManager.FindByIdAsync(id);
            if (user != null)
            {
                IdentityResult result = await UserManager.DeleteAsync(user);
                if (result.Succeeded)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    return View("Error", result.Errors);
                }

            }
            else
            {
                return View("Error", new string[] { "User not found" });
            }
        }

        [ChildActionOnly]
        public  ActionResult UserWithoutRoles()
        {
            var users = UserManager.Users.ToList();

            var userWithoutRolesModel = users.Where(x => x.Roles.Count == 0).ToList();

            return PartialView("UserWithoutRoles", userWithoutRolesModel);

        }
    }
}