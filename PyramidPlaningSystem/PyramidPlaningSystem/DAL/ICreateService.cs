using PyramidPlaningSystem.Models;
using PyramidPlaningSystem.ViewModels;

namespace PyramidPlaningSystem.DAL
{
    public interface ICreateService
    {
        void CreateAndAddChildToDo(ToDoModel toDoModel, ToDo childToDo);
        void CreateAndAddParentToDo(ToDoModel toDoModel);
        void CreateAndAddAssignment(ToDo toDo, ApplicationUser user);
        void ManageChildTodos(ToDoModel toDoModel);
        void ManageParentTodo(ToDoModel toDoModel);
    }
}