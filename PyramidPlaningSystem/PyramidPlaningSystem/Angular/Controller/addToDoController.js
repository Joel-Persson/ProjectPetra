
myApp.controller('addToDoController', function ($scope, toDoFactory, $mdDialog, $location) {
    $scope.PageHeader = "Add Todo";
    $scope.Time = "";
    $scope.ToDoModel = {
        ParentToDo: {},
        ChildToDos: []
    };


    $scope.add = function (ToDoModel) {

        if (ToDoModel.ParentToDo.Time == "days") {
            ToDoModel.ParentToDo.Effort = ToDoModel.ParentToDo.Effort * 8;
        }
        for (var i = 0; i < ToDoModel.ChildToDos.length; i++) {
            if (ToDoModel.ChildToDos[i].Time == "days") {
                ToDoModel.ChildToDos[i].Effort = ToDoModel.ChildToDos[i].Effort * 8;
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
            $scope.ToDoModel.ChildToDos.splice($scope.ToDoModel.ChildToDos.indexOf(subItem), 1);
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



