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
    toDoFactory.editToDo = function (toDo) {
        return $http.put(urlBase + "/" + toDo.ToDoId, toDo);
    }

    return toDoFactory;
});

myApp.factory('formatDateFactory', function () {

    var formatDateFactory = {};

    formatDateFactory.formatDate = function (date) {
        var d = new Date(date),
               month = '' + (d.getMonth() + 1),
               day = '' + d.getDate(),
               year = d.getFullYear();

        if (month.length < 2) month = '0' + month;
        if (day.length < 2) day = '0' + day;

        return [year, month, day].join('-');
    };

    return formatDateFactory;

});

