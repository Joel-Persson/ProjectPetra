﻿using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Microsoft.Ajax.Utilities;
using PyramidPlaningSystem.Authorize;
using PyramidPlaningSystem.Models;
using PyramidPlaningSystem.ViewModels;

namespace PyramidPlaningSystem.API
{
    [Authorize(Roles = "Administrator")]
    // [CustomAuthorize(Roles = "Production, Administrator")]
    public class ToDoController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        [HttpGet]
        public IEnumerable<ToDo> Get()
        {
            return db.ToDos.AsEnumerable();
        }

        public ToDoModel Get(Guid id)
        {

            ToDo parentToDoModel = db.ToDos.Find(id);

            if (parentToDoModel == null)
            {
                throw new HttpResponseException(Request.CreateResponse(HttpStatusCode.NotFound));
            }

            var childTodods = db.ToDos.Where(x => x.ParentId == id).ToList();
            var toDoModel = new ToDoModel { ParentToDo = parentToDoModel, ChildToDos = childTodods };
            return toDoModel;
        }

        //public ToDo Get(Guid id)
        //{
        //    ToDo toDoModel = db.ToDos.Find(id);
        //    if (toDoModel == null)
        //    {
        //        throw new HttpResponseException(Request.CreateResponse(HttpStatusCode.NotFound));
        //    }

        //    return toDoModel;
        //}


        [HttpPost]
        public HttpResponseMessage Post(ToDoModel toDoModel)
        {
            if (ModelState.IsValid)
            {
                if (toDoModel.ParentToDo.ToDoId == Guid.Empty)
                {
                    toDoModel.ParentToDo.Created = DateTime.Now;
                    db.ToDos.Add(toDoModel.ParentToDo);
                    db.SaveChanges();
                }


                if (toDoModel.ChildToDos.Any())
                {
                    foreach (var childToDo in toDoModel.ChildToDos)
                    {
                        if (childToDo.ToDoId == Guid.Empty)
                        {
                            childToDo.ParentId = toDoModel.ParentToDo.ToDoId;
                            childToDo.Created = DateTime.Now;
                            db.ToDos.Add(childToDo);
                            db.SaveChanges();
                        }

                    }
                }

                HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.Created, toDoModel.ParentToDo);
                response.Headers.Location = new Uri(Url.Link("DefaultApi", new { id = toDoModel.ParentToDo.ToDoId }));
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

        public HttpResponseMessage Delete(Guid id)
        {
            ToDo toDoModel = db.ToDos.Find(id);

            var toDos = (from b in db.ToDos
                         where b.ParentId == id
                         select b).ToList();

            if (toDoModel == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }
            if (toDos.Any())
            {
                foreach (var toDo in toDos)
                {
                    db.ToDos.Remove(toDo);
                }
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
