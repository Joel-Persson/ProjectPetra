

myApp.controller('editToDoController', function ($scope, $routeParams, toDoFactory, $location, $mdDialog) {
    $scope.PageHeader = "Edit ToDo";

    getSingleToDo();
    function getSingleToDo() {
        toDoFactory.getSingleToDo($routeParams.Id).success(function (data) {
            $scope.toDoModel = data;
        })
        .error(function () {
            $scope.status = "Something went wrong!";
        });

    };

    $scope.editToDo = function (toDo) {
        toDoFactory.editToDo(toDo).success(function () {
            $location.path('/toDos');
        });
    };
});