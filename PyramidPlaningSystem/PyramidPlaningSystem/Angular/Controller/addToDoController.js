myApp.controller('addToDoController', function ($scope, toDoFactory, $mdDialog, $location) {

    $scope.time = "";
    $scope.ToDoModel = {
        ParentToDo: {},
        ChildToDos: []
    };


    $scope.add = function (ToDoModel) {

        if (ToDoModel.ParentToDo.time == "days") {
            ToDoModel.ParentToDo.effort = ToDoModel.ParentToDo.effort * 8;
        }
        for (var i = 0; i < ToDoModel.ChildToDos.length; i++) {
            if (ToDoModel.ChildToDos[i].time == "days") {
                ToDoModel.ChildToDos[i].effort = ToDoModel.ChildToDos[i].effort * 8;
            }
        }

        toDoFactory.addToDo(ToDoModel).success(function () {
            $scope.success = "Yes";
            $location.path('/toDos');
        })
            .error(function () {
                $scope.success = "No";
            });

    };

    $scope.ShowAddSubToDo = function (ev) {
        $mdDialog.show({
            controller: DialogController,
            templateUrl: '/Angular/HtmlTemplates/addSubToDo.html',
            targetEvent: ev,

        }).then(function (subItem) {
            $scope.ToDoModel.ChildToDos.push(subItem);

        });
    };

    $scope.showConfirm = function (ev, subItem) {
        var confirm = $mdDialog.confirm()
          .title('Confirm')
          .content('Are you sure you want to delete the sub task?')
          .ok('Yes')
          .cancel('No')
          .targetEvent(ev);
        $mdDialog.show(confirm).then(function () {
            $scope.ToDoModel.ChildToDos.splice($scope.ToDoModel.ChildToDos.indexOf($scope.subItem), 1);
            //$scope.ToDoModel.ChildToDos.splice($index, 1);
        }, function () {
            $mdDialog.cancel();
        });
    };

    function DialogController($scope, $mdDialog) {

        $scope.cancel = function () {
            $mdDialog.cancel();
        };
        $scope.addSubItem = function (subItem) {
            $mdDialog.hide(subItem);
        };
    };

});



myApp.controller('listAllToDosController', function ($scope, toDoFactory, $mdDialog, $location) {
    $scope.toDoList = [];
    getTodos();

    function getTodos() {
        toDoFactory.getToDoList().success(function (data) {
            $scope.toDoList = data;
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
            $scope.deleteToDo(toDo.ToDoId);
            //bästa lösningen för att ta bort ett item i en array
            $scope.toDoList.splice($scope.toDoList.indexOf($scope.toDo), 1);

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
