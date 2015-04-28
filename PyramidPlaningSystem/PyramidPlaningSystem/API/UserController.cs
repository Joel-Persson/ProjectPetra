using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Owin.Security.Providers.ArcGISOnline.Provider;
using PyramidPlaningSystem.Models;

namespace PyramidPlaningSystem.API
{
    public class UserController : ApiController
    {
        private ApplicationUserManager UserManager
        {
            get { return HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>(); }
        }

        [HttpGet]
        public ApplicationUser Get()
        {
            var currentUser = UserManager.FindById(HttpContext.Current.User.Identity.GetUserId());

            return currentUser;
        }
    }
}
