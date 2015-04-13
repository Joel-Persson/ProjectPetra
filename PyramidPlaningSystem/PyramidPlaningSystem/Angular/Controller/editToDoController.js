﻿

myApp.controller('editToDoController', function ($scope, $routeParams, toDoFactory, $location, $mdDialog) {
    $scope.PageHeader = "Edit ToDo";

    $scope.oneAtATime = true;

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



    $scope.ShowAddSubToDo = function (ev) {
        $mdDialog.show({
            controller: DialogController,
            templateUrl: '/Angular/HtmlTemplates/addSubToDo.html',
            targetEvent: ev,

        }).then(function (subItem) {
            $scope.toDoModel.ChildToDos.push(subItem);
            $scope.editSubToDo($scope.toDoModel);
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

    $scope.deleteConfirm = function (ev, toDo) {
        var confirm = $mdDialog.confirm()
          .title('Confirm')
          .content('Are you sure you want to delete the ToDo?')
          .ok('Yes')
          .cancel('No')
          .targetEvent(ev);
        $mdDialog.show(confirm).then(function () {
            $scope.toDoModel.ChildToDos.splice($scope.toDoModel.ChildToDos.indexOf(toDo), 1);
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

    $scope.editSubToDo = function (toDoModel) {
        if (toDoModel.ParentToDo.Time == "days") {
            toDoModel.ParentToDo.Effort = toDoModel.ParentToDo.Effort * 8;
        }
        for (var i = 0; i < toDoModel.ChildToDos.length; i++) {
            if (toDoModel.ChildToDos[i].Time == "days") {
                toDoModel.ChildToDos[i].Effort = toDoModel.ChildToDos[i].Effort * 8;
            }
        }

        toDoFactory.addToDo(toDoModel).success(function () {
            $scope.success = "Yes";
        })
            .error(function () {
                $scope.success = "No";
            });

    };
});
