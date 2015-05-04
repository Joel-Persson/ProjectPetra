myApp.factory("toDoFactory", function ($http) {
    var urlBase = "/api/ToDo";
    var toDoFactory = {};

    toDoFactory.getSingleToDo = function (id) {
        return $http.get(urlBase + "/GetTodo/" + id);
    }

    toDoFactory.addToDo = function (toDoModel) {
        return $http.post(urlBase, toDoModel);
    }
    toDoFactory.getToDoList = function () {
        return $http.get(urlBase);
    }
    toDoFactory.deleteToDo = function (id) {
        return $http.delete(urlBase + "/DeleteToDo/" + id);
    }
    toDoFactory.editToDo = function (toDoModel) {
        return $http.put(urlBase + "/EditToDo/" + toDoModel.ToDo.ToDoId, toDoModel);
    }

    return toDoFactory;
});


myApp.factory('userFactory', function ($http) {
    var urlBase = "/api/User";
    var userFactory = {};

    userFactory.getUser = function () {
        return $http.get(urlBase);
    }
    return userFactory;
});

myApp.factory('commentFactory', function ($http) {
    var urlBase = "/api/Comment";
    var commentFactory = {};

    commentFactory.addComment = function (comment) {
        return $http.post(urlBase + "/PostComment/", comment);
    };

    commentFactory.getCommentByToDoId = function (id) {
        return $http.get(urlBase + "/GetCommentsByToDoId/" + id);
    };

    return commentFactory;
});


myApp.factory("contactFactory", function ($http) {
    var urlBase = "/api/Contacts";
    var contactFactory = {};

    contactFactory.getContacts = function () {
        return $http.get(urlBase);
    };

    contactFactory.updateContactDetails = function (contactDetails) {
        return $http.put(urlBase + "/UpdateContactDetails/" + contactDetails.Id, contactDetails);
    };

    return contactFactory;
});

myApp.factory("assignmentFactory", function ($http) {
    var urlBase = "/api/Assignment";
    var assignmentFactory = {};

    assignmentFactory.addAssignment = function (assignment) {
        return $http.post(urlBase, assignment);
    }

    assignmentFactory.getAssignments = function (id) {
        return $http.get(urlBase + "/GetAssignmentsForToDo/" + id);
    }

    assignmentFactory.getAssignmentsForUser = function (id) {
        return $http.get(urlBase + "/GetUserAssignments/" + id);
    };

    return assignmentFactory;
});


