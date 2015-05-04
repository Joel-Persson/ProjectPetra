myApp.service('formatDateService', function () {

    var formatDate = function (date) {
        if (date != null) {
            var d = new Date(date),
                   month = '' + (d.getMonth() + 1),
                   day = '' + d.getDate(),
                   year = d.getFullYear();

            if (month.length < 2) month = '0' + month;
            if (day.length < 2) day = '0' + day;

            return [year, month, day].join('-');
        }
        return null;
    };

    var formatTime = function (ToDoModel) {
        if (ToDoModel.ParentToDo != null) {
            ToDoModel.ParentToDo.Deadline = formatDate(ToDoModel.ParentToDo.Deadline);
            ToDoModel.ParentToDo.StartDate = formatDate(ToDoModel.ParentToDo.StartDate);
            ToDoModel.ParentToDo.EndDate = formatDate(ToDoModel.ParentToDo.EndDate);
            if (ToDoModel.ParentToDo.Time == "days") {
                ToDoModel.ParentToDo.Effort = ToDoModel.ParentToDo.Effort * 8;
            }

            for (var i = 0; i < ToDoModel.ChildToDos.length; i++) {
                ToDoModel.ChildToDos[i].Deadline = formatDate(ToDoModel.ChildToDos[i].Deadline);
                ToDoModel.ChildToDos[i].StartDate = formatDate(ToDoModel.ChildToDos[i].StartDate);
                ToDoModel.ChildToDos[i].EndDate = formatDate(ToDoModel.ChildToDos[i].EndDate);
                if (ToDoModel.ChildToDos[i].Time == "days") {
                    ToDoModel.ChildToDos[i].Effort = ToDoModel.ChildToDos[i].Effort * 8;
                }
            }
        }
        else {
            ToDoModel.Deadline = formatDate(ToDoModel.Deadline);
            ToDoModel.StartDate = formatDate(ToDoModel.StartDate);
            ToDoModel.EndDate = formatDate(ToDoModel.EndDate);
            if (ToDoModel.Time == "days") {
                ToDoModel.Effort = ToDoModel.Effort * 8;
            }
        }

    }

    return {
        formatDate: formatDate,
        formatTime: formatTime
    };

});