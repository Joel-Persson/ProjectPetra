using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using PyramidPlaningSystem.Models;

namespace PyramidPlaningSystem.API
{
    //[Authorize(Roles = "Administrator")]
    public class ContactsController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        [HttpGet]
        public IEnumerable<Contact> Get()
        {
            return db.Contacts.AsEnumerable();
        }

    }
}
