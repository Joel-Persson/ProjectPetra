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
    public class ToDoController : ApiController
    {
         private ApplicationDbContext db = new ApplicationDbContext();

        [HttpGet]
        public IEnumerable<ToDo> Get()
        {
            return db.ToDos.AsEnumerable();
        }

        public ToDo Get(int id)
        {
            ToDo toDoModel = db.ToDos.Find(id);
            if (toDoModel == null)
            {
                throw new HttpResponseException(Request.CreateResponse(HttpStatusCode.NotFound));
            }

            return toDoModel;
        }

        public HttpResponseMessage Post(ToDo toDo)
        {
            if (ModelState.IsValid)
            {
                db.ToDos.Add(toDo);
                db.SaveChanges();

                HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.Created, toDo);
                response.Headers.Location = new Uri(Url.Link("DefaultApi", new { id = toDo.ToDoId}));
                return response;
            }
            else
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }
        }

        public HttpResponseMessage Put(Guid id, ToDo toDo)
        {
            if (!ModelState.IsValid)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }

            if (id != toDo.ToDoId)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }

            db.Entry(toDo).State = EntityState.Modified;

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

        public HttpResponseMessage Delete(int id)
        {
            ToDo toDoModel = db.ToDos.Find(id);
            if (toDoModel == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }

            db.ToDos.Remove(toDoModel);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, ex);
            }

            return Request.CreateResponse(HttpStatusCode.OK, toDoModel);
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        } 
    
    }
}
