myApp.factory("toDoFactory", function($http) {
    var urlBase = "/api/ToDo";
    var customBase = "/api/addSubToDo";
    var toDoFactory = {};

    toDoFactory.addToDo = function(toDo) {
        return $http.post(urlBase, toDo);
    }

    toDoFactory.getSingleToDo = function(id) {
        return $http.get(urlBase + "/" + id);
    }

    toDoFactory.addSubToDo = function (toDo, toDos) {
        return $http.post(customBase, toDo,toDos);
    }

    return toDoFactory;
})

