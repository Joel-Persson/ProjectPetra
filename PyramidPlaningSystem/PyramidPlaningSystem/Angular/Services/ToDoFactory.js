myApp.factory("toDoFactory", function ($http) {
    var urlBase = "/api/ToDo";
    var toDoFactory = {};

    toDoFactory.getSingleToDo = function (id) {
        return $http.get(urlBase + "/" + id);
    }

    toDoFactory.addToDo = function (toDoModel) {
        return $http.post(urlBase, toDoModel);
    }

    return toDoFactory;
})

