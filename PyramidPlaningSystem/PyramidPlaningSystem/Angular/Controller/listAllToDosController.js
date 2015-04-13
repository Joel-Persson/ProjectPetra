
myApp.controller('listAllToDosController', function ($scope, toDoFactory, $mdDialog) {
    $scope.PageHeader = "Listing all Todos";
    $scope.toDoList = [];
    $scope.ParentToDos = [];
    getTodos();

    function getTodos() {
        toDoFactory.getToDoList().success(function (data) {
            $scope.toDoList = data;
            $.each($scope.toDoList, function(i) {
                if ($scope.toDoList[i].ParentId == null) {
                    $scope.ParentToDos.push($scope.toDoList[i]);
                }
            });
        });
    }

    $scope.deleteConfirm = function (ev, toDo) {
        var confirm = $mdDialog.confirm()
          .title('Confirm')
          .content('Are you sure you want to delete the ToDo?')
          .ok('Yes')
          .cancel('No')
          .targetEvent(ev);
        $mdDialog.show(confirm).then(function () {
             $scope.ParentToDos.splice($scope.ParentToDos.indexOf(toDo), 1);
            $scope.deleteToDo(toDo.ToDoId);

        }, function () {
            $mdDialog.cancel();
        });
    };

    $scope.deleteToDo = function (toDoId) {
        toDoFactory.deleteToDo(toDoId).success(function () {
            $scope.status = "Deleted";
        }).error(function () {
            $scope.status = "Error";
        });
    };

});