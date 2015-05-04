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

    var formatTime = function (inputModel) {

        if (inputModel.ParentToDo != null) {

            inputModel.ParentToDo.Deadline = formatDate(inputModel.ParentToDo.Deadline);
            inputModel.ParentToDo.StartDate = formatDate(inputModel.ParentToDo.StartDate);
            inputModel.ParentToDo.EndDate = formatDate(inputModel.ParentToDo.EndDate);

            if (inputModel.ParentToDo.Time == "days") {
                inputModel.ParentToDo.Effort = inputModel.ParentToDo.Effort * 8;
            }

            for (var i = 0; i < inputModel.ChildToDos.length; i++) {
                inputModel.ChildToDos[i].Deadline = formatDate(inputModel.ChildToDos[i].Deadline);
                inputModel.ChildToDos[i].StartDate = formatDate(inputModel.ChildToDos[i].StartDate);
                inputModel.ChildToDos[i].EndDate = formatDate(inputModel.ChildToDos[i].EndDate);
                if (inputModel.ChildToDos[i].Time == "days") {
                    inputModel.ChildToDos[i].Effort = inputModel.ChildToDos[i].Effort * 8;
                }
            }
        } else {
            inputModel.Deadline = formatDate(inputModel.Deadline);
            inputModel.StartDate = formatDate(inputModel.StartDate);
            inputModel.EndDate = formatDate(inputModel.EndDate);
            if (inputModel.Time == "days") {
                inputModel.Effort = inputModel.Effort * 8;
            }
        }

    };

    return {
        formatDate: formatDate,
        formatTime: formatTime
    };

});