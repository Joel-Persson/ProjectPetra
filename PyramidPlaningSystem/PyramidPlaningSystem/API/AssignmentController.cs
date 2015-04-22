using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using PyramidPlaningSystem.Models;

namespace PyramidPlaningSystem.API
{
     [Authorize(Roles = "Administrator")]
    public class AssignmentController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        [HttpGet]
        public IEnumerable<Assignment> Get()
        {
            return db.Assignments.AsEnumerable();
        }

        public Assignment Get(Guid id)
        {
            Assignment assignment = db.Assignments.Find(id);

            if (assignment == null)
            {
                throw new HttpResponseException(Request.CreateResponse(HttpStatusCode.NotFound));
            }

            return assignment;
        }

        [HttpPost]
        public HttpResponseMessage Post(Assignment assignment)
        {
            if (ModelState.IsValid)
            {

                if (assignment.Id == Guid.Empty)
                {
                    assignment.TimeStamp= DateTime.Now;
                    db.Assignments.Add(assignment);
                    db.SaveChanges();
                }

                HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.Created, assignment.Id);
                response.Headers.Location = new Uri(Url.Link("DefaultApi", new { id = assignment.Id}));
                return response;
            }
            else
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }
        }

        public HttpResponseMessage Put(Guid id, Assignment assignment)
        {
            if (!ModelState.IsValid)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }

            if (id != assignment.Id)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }

            db.Entry(assignment).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, ex);
            }

            return Request.CreateResponse(HttpStatusCode.OK);
        }

        public HttpResponseMessage Delete(Guid id)
        {
            Assignment assignment = db.Assignments.Find(id);


            if (assignment == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }

            //assignment.Deleted = true;
            db.Assignments.Remove(assignment);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, ex);
            }

            return Request.CreateResponse(HttpStatusCode.OK, assignment);
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}