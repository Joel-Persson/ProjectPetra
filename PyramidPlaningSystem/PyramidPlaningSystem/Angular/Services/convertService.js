myApp.service('convertService', function () {

    var convertTodo = function (inputModel) {
        var toDo = {};
        toDo.Title = inputModel.Title;
        toDo.Description = inputModel.Description;
        toDo.Effort = inputModel.Effort;
        toDo.Deadline = inputModel.Deadline;
        toDo.EndDate = inputModel.EndDate;
        toDo.StartDate = inputModel.StartDate;
        toDo.Priority = inputModel.Priority;
        return toDo;
    };

    var convertTimeVariablesToDateObject = function (inputModel) {
        inputModel.ParentToDo.ToDo.Deadline = new Date(inputModel.ParentToDo.ToDo.Deadline);
        inputModel.ParentToDo.ToDo.StartDate = new Date(inputModel.ParentToDo.ToDo.StartDate);
        inputModel.ParentToDo.ToDo.EndDate = new Date(inputModel.ParentToDo.ToDo.EndDate);

        $.each(inputModel.ChildToDos, function (i) {
            inputModel.ChildToDos[i].ToDo.Deadline = new Date(inputModel.ChildToDos[i].ToDo.Deadline);
            inputModel.ChildToDos[i].ToDo.StartDate = new Date(inputModel.ChildToDos[i].ToDo.StartDate);
            inputModel.ChildToDos[i].ToDo.EndDate = new Date(inputModel.ChildToDos[i].ToDo.EndDate);
        });
        return inputModel;
    }

    return {
        convertTodo: convertTodo,
        convertTimeVariablesToDateObject: convertTimeVariablesToDateObject
    };
});