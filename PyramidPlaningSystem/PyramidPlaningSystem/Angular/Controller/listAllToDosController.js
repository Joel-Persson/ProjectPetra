
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

    $scope.deleteConfirm = function (ev, $index, toDo) {
        var confirm = $mdDialog.confirm()
          .title('Confirm')
          .content('Are you sure you want to delete the ToDo?')
          .ok('Yes')
          .cancel('No')
          .targetEvent(ev);
        $mdDialog.show(confirm).then(function () {
            //bästa lösningen för att ta bort ett item i en array
            $.each($scope.toDoList, function (i) {
                if ($scope.toDoList[i].ParentId === toDo.ToDoId) {
                    $scope.toDoList.splice(i, 1);
                }
            });
            $scope.index = $index;
            $scope.toDoList.splice($index, 1);
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