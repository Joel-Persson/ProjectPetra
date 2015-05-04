myApp.service('convertService', function () {

    var convertTodo = function (ToDoModel) {
        var ToDo = {};
        ToDo.Title = ToDoModel.Title;
        ToDo.Description = ToDoModel.Description;
        ToDo.Effort = ToDoModel.Effort;
        ToDo.Deadline = ToDoModel.Deadline;
        ToDo.EndDate = ToDoModel.EndDate;
        ToDo.StartDate = ToDoModel.StartDate;
        ToDo.Priority = ToDoModel.Priority;
        return ToDo;
    };

    var convertTimeVariablesToDateObject = function (toDoModel) {
        toDoModel.ParentToDo.ToDo.Deadline = new Date(toDoModel.ParentToDo.ToDo.Deadline);
        toDoModel.ParentToDo.ToDo.StartDate = new Date(toDoModel.ParentToDo.ToDo.StartDate);
        toDoModel.ParentToDo.ToDo.EndDate = new Date(toDoModel.ParentToDo.ToDo.EndDate);

        $.each(toDoModel.ChildToDos, function (i) {
            toDoModel.ChildToDos[i].ToDo.Deadline = new Date(toDoModel.ChildToDos[i].ToDo.Deadline);
            toDoModel.ChildToDos[i].ToDo.StartDate = new Date(toDoModel.ChildToDos[i].ToDo.StartDate);
            toDoModel.ChildToDos[i].ToDo.EndDate = new Date(toDoModel.ChildToDos[i].ToDo.EndDate);
        });
        return toDoModel;
    }

    return {
        convertTodo: convertTodo,
        convertTimeVariablesToDateObject: convertTimeVariablesToDateObject
    };
});