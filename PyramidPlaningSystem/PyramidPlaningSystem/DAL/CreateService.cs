using System;
using System.Linq;
using System.Web;
using Microsoft.AspNet.Identity.EntityFramework;
using Owin.Security.Providers.ArcGISOnline.Provider;
using PyramidPlaningSystem.Models;
using PyramidPlaningSystem.ViewModels;
using Microsoft.AspNet.Identity;
using System.Web.Mvc;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Host.SystemWeb;

namespace PyramidPlaningSystem.DAL
{
    public class CreateService : ICreateService
    {

        private readonly ApplicationDbContext _db;

        private ApplicationUserManager UserManager
        {
            get { return HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>(); }
        }

        public CreateService(ApplicationDbContext db)
        {
            _db = db;
        }

        public void CreateAndAddChildToDo(ToDoModel toDoModel, ToDo childToDo)
        {
            childToDo.ParentId = toDoModel.ParentToDo.ToDo.ToDoId;
            childToDo.Created = DateTime.Now;
            _db.ToDos.Add(childToDo);
        }

        public void CreateAndAddParentToDo(ToDo toDoModel)
        {
            toDoModel.Created = DateTime.Now;
            _db.ToDos.Add(toDoModel);
        }

        public void CreateAndAddAssignment(ToDo toDo, ApplicationUser user)
        {
            var result = _db.Assignments.SingleOrDefault(x => x.User.Id == user.Id && x.Todo.ToDoId == toDo.ToDoId);

            if (result == null)
            {
                var addedBy = UserManager.FindById(HttpContext.Current.User.Identity.GetUserId());

                var assignment = new Assignment
                {
                    TimeStamp = DateTime.Now,
                    Todo = toDo,
                    AddedBy = addedBy.Contact.Firstname + " " + addedBy.Contact.Lastname,
                    User = user
                };

                _db.Assignments.Add(assignment);
            }

        }

        public void ManageChildTodos(ToDoModel toDoModel)
        {
            if (toDoModel.ChildToDos != null && toDoModel.ChildToDos.Any())
            {
                foreach (var childToDo in toDoModel.ChildToDos)
                {
                    if (childToDo.ToDo.ToDoId == Guid.Empty)
                    {
                        CreateAndAddChildToDo(toDoModel, childToDo.ToDo);
                    }
                }
                _db.SaveChanges();

                foreach (var childToDo in toDoModel.ChildToDos)
                {
                    if (childToDo.ContactIdList != null && childToDo.ContactIdList.Any())
                    {
                        foreach (var item in childToDo.ContactIdList)
                        {
                            var contactId = int.Parse(item);
                            var user = _db.Users.FirstOrDefault(x => x.Contact.Id == contactId);
                            if (user != null)
                            {
                                CreateAndAddAssignment(childToDo.ToDo, user);
                            }
                        }
                    }
                }

                _db.SaveChanges();
            }
        }

        public void ManageParentTodo(ToDoViewModel toDoModel)
        {
            if (toDoModel.ToDo.ToDoId == Guid.Empty)
            {
                CreateAndAddParentToDo(toDoModel.ToDo);
                _db.SaveChanges();
            }
            if (toDoModel.ContactIdList != null && toDoModel.ContactIdList.Any())
            {
                foreach (var item in toDoModel.ContactIdList)
                {
                    var contactId = int.Parse(item);
                    var user = _db.Users.FirstOrDefault(x => x.Contact.Id == contactId);
                    if (user != null)
                    {
                        CreateAndAddAssignment(toDoModel.ToDo, user);
                    }
                }
                _db.SaveChanges();
            }

        }
    }
}