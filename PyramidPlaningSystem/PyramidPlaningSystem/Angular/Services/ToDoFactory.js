myApp.factory("toDoFactory", function($http) {
    var urlBase = "/api/ToDo";
    var toDoFactory = {};

    toDoFactory.addToDo = function(toDo) {
        return $http.post(urlBase, toDo);
    }

    return toDoFactory;
})