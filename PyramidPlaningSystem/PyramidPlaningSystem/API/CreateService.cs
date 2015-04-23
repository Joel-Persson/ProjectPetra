using System;
using System.Linq;
using PyramidPlaningSystem.Models;
using PyramidPlaningSystem.ViewModels;

namespace PyramidPlaningSystem.API
{
    public interface ICreateService
    {
        void CreateAndAddChildToDo(ToDoModel toDoModel, ToDo childToDo);
        void CreateAndAddParentToDo(ToDoModel toDoModel);
        void CreateAndAddAssignment(ToDo toDo, ApplicationUser user);
        void ManageChildTodos(ToDoModel toDoModel);
        void ManageParentTodo(ToDoModel toDoModel);
    }

    public class CreateService : ICreateService
    {
  
        private readonly ApplicationDbContext _db;


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

        public void CreateAndAddParentToDo(ToDoModel toDoModel)
        {
            toDoModel.ParentToDo.ToDo.Created = DateTime.Now;
            _db.ToDos.Add(toDoModel.ParentToDo.ToDo);
        }

        public void CreateAndAddAssignment(ToDo toDo, ApplicationUser user)
        {
            var assignment = new Assignment
            {
                TimeStamp = DateTime.Now,
                Todo = toDo,
                AddedBy = "Admin",
                User = user
            };

            _db.Assignments.Add(assignment);
        }

        public void ManageChildTodos(ToDoModel toDoModel)
        {
            if (toDoModel.ChildToDos.Any())
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
                    if (childToDo.ContactIdList != null)
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

        public void ManageParentTodo(ToDoModel toDoModel)
        {
            if (toDoModel.ParentToDo.ToDo.ToDoId == Guid.Empty)
            {
                CreateAndAddParentToDo(toDoModel);
                _db.SaveChanges();

                if (toDoModel.ParentToDo.ContactIdList.Any())
                {
                    foreach (var contactId in toDoModel.ParentToDo.ContactIdList)
                    {
                        var userId = int.Parse(contactId);
                        var user = _db.Users.FirstOrDefault(x => x.Contact.Id == userId);
                        if (user != null)
                        {
                            CreateAndAddAssignment(toDoModel.ParentToDo.ToDo, user);
                        }
                    }
                    _db.SaveChanges();
                }
            }
        }
    }
}