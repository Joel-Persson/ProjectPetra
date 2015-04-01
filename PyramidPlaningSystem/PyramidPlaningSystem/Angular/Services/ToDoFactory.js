myApp.factory("toDoFactory", function($http) {
    var urlBase = "/api/ToDo";
    var toDoFactory = {};

    toDoFactory.addToDo = function(toDo) {
        return $http.post(urlBase, toDo);
    }

    toDoFactory.getSingleToDo = function(id) {
        return $http.get(urlBase + "/" + id);
    }

    return toDoFactory;
})

