using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using PyramidPlaningSystem.Models;
using PyramidPlaningSystem.ViewModels;

namespace PyramidPlaningSystem.API
{
    [Authorize(Roles = "Administrator")]
    // [CustomAuthorize(Roles = "Production, Administrator")]
    public class ToDoController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        private ApplicationUserManager UserManager
        {
            get { return HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>(); }
        }


        [HttpGet]
        public IEnumerable<ToDo> Get()
        {
            return db.ToDos.Where(x => x.Deleted == false).AsEnumerable();
        }

        public ToDoModel Get(Guid id)
        {

            ToDo parentToDoModel = db.ToDos.Find(id);

            if (parentToDoModel == null)
            {
                throw new HttpResponseException(Request.CreateResponse(HttpStatusCode.NotFound));
            }

            var childTodods = db.ToDos.Where(x => x.ParentId == id && x.Deleted == false).ToList();

            var toDoModel = new ToDoModel();
            toDoModel.ParentToDo.ToDo = parentToDoModel;

            foreach (var item in childTodods)
            {
                toDoModel.ChildToDos.Add(new ToDoViewModel()
                {
                    ToDo = item
                });
            }
            //toDoModel.ChildToDos = childTodods.ToList();
            return toDoModel;
        }

        [HttpPost]
        public HttpResponseMessage Post(ToDoModel toDoModel)
        {
            if (ModelState.IsValid)
            {
                if (toDoModel.ParentToDo.ToDo.ToDoId == Guid.Empty)
                {
                    CreateAndAddParentToDo(toDoModel);
                    db.SaveChanges();
                }
                if (toDoModel.ParentToDo.ContactIdList.Any())
                {
                    foreach (var contactId in toDoModel.ParentToDo.ContactIdList)
                    {
                        var userId = int.Parse(contactId);
                        var user = db.Users.FirstOrDefault(x => x.Contact.Id == userId);
                        if (user != null)
                        {
                            CreateAndAddAssignment(toDoModel.ParentToDo.ToDo, user);
                        }
                    }
                    db.SaveChanges();
                }

                if (toDoModel.ChildToDos.Any())
                {
                    foreach (var childToDo in toDoModel.ChildToDos)
                    {
                        if (childToDo.ToDo.ToDoId == Guid.Empty)
                        {
                            CreateAndAddChildToDo(toDoModel, childToDo.ToDo);
                          
                        }
                    }
                    db.SaveChanges();

                    foreach (var childToDo in toDoModel.ChildToDos)
                    {
                        if (childToDo.ContactIdList.Any())
                        {
                            foreach (var item in childToDo.ContactIdList)
                            {
                                var contactId = int.Parse(item);
                                var user = db.Users.FirstOrDefault(x => x.Contact.Id == contactId);
                                if (user != null)
                                {
                                    CreateAndAddAssignment(childToDo.ToDo, user);
                                }
                            }
                        }
                    }

                    db.SaveChanges();
                }

                HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.Created, toDoModel.ParentToDo);
                response.Headers.Location = new Uri(Url.Link("DefaultApi", new { id = toDoModel.ParentToDo.ToDo.ToDoId }));
                return response;
            }
            else
            {

                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }
        }

        private void CreateAndAddChildToDo(ToDoModel toDoModel, ToDo childToDo)
        {
            childToDo.ParentId = toDoModel.ParentToDo.ToDo.ToDoId;
            childToDo.Created = DateTime.Now;
            db.ToDos.Add(childToDo);
        }

        private void CreateAndAddParentToDo(ToDoModel toDoModel)
        {
            toDoModel.ParentToDo.ToDo.Created = DateTime.Now;
            db.ToDos.Add(toDoModel.ParentToDo.ToDo);
        }

        private void CreateAndAddAssignment(ToDo toDo, ApplicationUser user)
        {
            var assignment = new Assignment
            {
                TimeStamp = DateTime.Now,
                Todo = toDo,
                AddedBy = "Admin",
                User = user
            };

            db.Assignments.Add(assignment);
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
            ToDo parentToDo = db.ToDos.Find(id);

            var childToDos = (from b in db.ToDos
                              where b.ParentId == id
                              select b).ToList();

            if (parentToDo == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }
            if (childToDos.Any())
            {
                foreach (var toDo in childToDos)
                {
                    toDo.Deleted = true;
                }
            }
            parentToDo.Deleted = true;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, ex);
            }

            return Request.CreateResponse(HttpStatusCode.OK, parentToDo);
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }

    }
}
