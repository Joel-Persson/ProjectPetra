myApp.factory("toDoFactory", function ($http) {
    var urlBase = "/api/ToDo";
    var toDoFactory = {};

    toDoFactory.getSingleToDo = function (id) {
        return $http.get(urlBase + "/" + id);
    }

    toDoFactory.addToDo = function (toDoModel) {
        return $http.post(urlBase, toDoModel);
    }
    toDoFactory.getToDoList = function () {
        return $http.get(urlBase);
    }
    toDoFactory.deleteToDo = function (id) {
        return $http.delete(urlBase + "/" + id);
    }
    toDoFactory.editToDo = function (toDoModel) {
        return $http.put(urlBase + "/" + toDoModel.ToDo.ToDoId, toDoModel);
    }

    return toDoFactory;
});

myApp.factory('userFactory', function($http) {
    var urlBase = "/api/User";
    var userFactory = {};

    userFactory.getUser = function() {
        return $http.get(urlBase);
    }
    return userFactory;
});

myApp.factory('formatDateFactory', function () {

    var formatDateFactory = {};

    formatDateFactory.formatDate = function (date) {
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

    formatDateFactory.formatTime = function (ToDoModel) {
        if (ToDoModel.ParentToDo != null) {
            ToDoModel.ParentToDo.Deadline = formatDateFactory.formatDate(ToDoModel.ParentToDo.Deadline);
            ToDoModel.ParentToDo.StartDate = formatDateFactory.formatDate(ToDoModel.ParentToDo.StartDate);
            ToDoModel.ParentToDo.EndDate = formatDateFactory.formatDate(ToDoModel.ParentToDo.EndDate);
            if (ToDoModel.ParentToDo.Time == "days") {
                ToDoModel.ParentToDo.Effort = ToDoModel.ParentToDo.Effort * 8;
            }

            for (var i = 0; i < ToDoModel.ChildToDos.length; i++) {
                ToDoModel.ChildToDos[i].Deadline = formatDateFactory.formatDate(ToDoModel.ChildToDos[i].Deadline);
                ToDoModel.ChildToDos[i].StartDate = formatDateFactory.formatDate(ToDoModel.ChildToDos[i].StartDate);
                ToDoModel.ChildToDos[i].EndDate = formatDateFactory.formatDate(ToDoModel.ChildToDos[i].EndDate);
                if (ToDoModel.ChildToDos[i].Time == "days") {
                    ToDoModel.ChildToDos[i].Effort = ToDoModel.ChildToDos[i].Effort * 8;
                }
            }
        }
        else {
            ToDoModel.Deadline = formatDateFactory.formatDate(ToDoModel.Deadline);
            ToDoModel.StartDate = formatDateFactory.formatDate(ToDoModel.StartDate);
            ToDoModel.EndDate = formatDateFactory.formatDate(ToDoModel.EndDate);
            if (ToDoModel.Time == "days") {
                ToDoModel.Effort = ToDoModel.Effort * 8;
            }
        }

    }

    return formatDateFactory;

});

myApp.factory("contactFactory", function ($http) {
    var urlBase = "/api/Contacts";
    var contactFactory = {};

    contactFactory.getContacts = function () {
        return $http.get(urlBase);
    };

    contactFactory.updateContactDetails = function(contactDetails) {
        return $http.put(urlBase + "/" + contactDetails.Id, contactDetails);
    };

    return contactFactory;
});

myApp.factory("assignmentFactory", function ($http) {
    var urlBase = "/api/Assignment";
    var assignmentFactory = {};

    assignmentFactory.addAssignment = function (assignment) {
        return $http.post(urlBase, assignment);
    }

    assignmentFactory.getAssignments = function(id) {
        return $http.get(urlBase + "/GetAssignmentsForToDo/" + id);
    }

    assignmentFactory.getAssignmentsForUser = function(id) {
        return $http.get(urlBase + "/GetUserAssignments/" + id);
    };

    return assignmentFactory;
});

myApp.service('tagService', function () {
    var tagList = [];
    var childTagList = [];

    var addTags = function (tags) {
        tagList = tags;
    };

    var getTags = function () {
        return tagList;
    }

    var addChildTags = function (tags) {
        childTagList = tags;
    };

    var getChildTags = function () {
        return childTagList;
    }

    return {
        addTags: addTags,
        getTags: getTags,
        addChildTags: addChildTags,
        getChildTags: getChildTags
    };
});

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

    var convertTimeVariablesToDateObject =  function (toDoModel) {
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