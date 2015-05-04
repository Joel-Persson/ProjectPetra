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
using PyramidPlaningSystem.DAL;
using PyramidPlaningSystem.Models;
using PyramidPlaningSystem.ViewModels;

namespace PyramidPlaningSystem.API
{
    [Authorize(Roles = "Administrator")]
    // [CustomAuthorize(Roles = "Production, Administrator")]
    public class ToDoController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        private readonly ICreateService _createService;
        private readonly IConvertService _convertService;
        public ToDoController()
        {
            _createService = new CreateService(db);
            _convertService = new ConvertService(db);
        }

        private ApplicationUserManager UserManager
        {
            get { return HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>(); }
        }


        [HttpGet]
        public IEnumerable<ToDo> Get()
        {
            return db.ToDos.Where(x => x.Deleted == false).AsEnumerable();
        }

        public ToDoModel GetTodo(Guid id)
        {

            ToDo parentToDoModel = db.ToDos.Find(id);

            if (parentToDoModel == null)
            {
                throw new HttpResponseException(Request.CreateResponse(HttpStatusCode.NotFound));
            }

            var parentTodo = new ToDoViewModel
            {
                ToDo = parentToDoModel
            };

            var childToDos = _convertService.ConvertChildTodos(id);

            var toDoModel = new ToDoModel
            {
                ParentToDo = parentTodo,
                ChildToDos = childToDos
            };

            return toDoModel;
        }

      

        [HttpPost]
        public HttpResponseMessage Post(ToDoModel toDoModel)
        {
            if (ModelState.IsValid)
            {
                _createService.ManageParentTodo(toDoModel.ParentToDo);

                _createService.ManageChildTodos(toDoModel);

                HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.Created, toDoModel.ParentToDo);
                response.Headers.Location = new Uri(Url.Link("DefaultApi", new { id = toDoModel.ParentToDo.ToDo.ToDoId }));
                return response;
            }
            else
            {

                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }
        }

        [HttpPut]
        public HttpResponseMessage EditToDo(Guid id, ToDoViewModel toDoViewModel)
        {
            if (!ModelState.IsValid)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }

            if (id != toDoViewModel.ToDo.ToDoId)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }

            db.Entry(toDoViewModel.ToDo).State = EntityState.Modified;

            _createService.ManageParentTodo(toDoViewModel);

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

        public HttpResponseMessage DeleteToDo(Guid id)
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
