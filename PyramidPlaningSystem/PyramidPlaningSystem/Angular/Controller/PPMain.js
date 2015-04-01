var myApp = angular.module("myApp", ['ngMaterial', 'ngAnimate', 'ngRoute', 'ngMessages']);

myApp.controller('mainLayoutController', function ($scope, $mdSidenav, $window) {

    $scope.toggleSidenav = function (menuId) {
        $mdSidenav(menuId).toggle();
    }

    $scope.getWidth = function () {
        return $(window).width();
    };
    $scope.$watch($scope.getWidth, function (newValue, oldValue) {
        $scope.windowWidth = newValue;

    });
    window.onresize = function () {
        $scope.$apply();
    }

    $scope.showAndHideBool = false;
    $scope.showAndHideButtonName = "Show";

    $scope.toggle = function (showAndHideBool) {

        if (showAndHideBool === false) {
            $scope.showAndHideBool = true;
            $scope.showAndHideButtonName = "Close";
        } else {
            $scope.showAndHideBool = false;
            $scope.showAndHideButtonName = "Show";
        }
    }
});


myApp.controller('leftController', function ($scope, $mdSidenav) {

    $scope.close = function () {
        $mdSidenav('left').close();
    }

});


myApp.controller('addToDoController', function ($scope, toDoFactory, $mdDialog) {
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
        })
            .error(function () {
                $scope.success = "No";
            });

    }

    $scope.ShowAddSubToDo = function (ev) {
        $mdDialog.show({
            controller: DialogController,
            templateUrl: '/Angular/HtmlTemplates/addSubToDo.html',
            targetEvent: ev,

        }).then(function (subItem) {
            $scope.ToDoModel.ChildToDos.push(subItem);

        });
    }

    function DialogController($scope, $mdDialog) {

        $scope.cancel = function () {
            $mdDialog.cancel();
        };
        $scope.addSubItem = function (subItem) {
            $mdDialog.hide(subItem);
        };
    };

    $scope.showConfirm = function (ev, $index) {
        var confirm = $mdDialog.confirm()
          .title('Confirm')
          .content('Are you sure you want to delete the sub task?')
          .ok('Yes')
          .cancel('No')
          .targetEvent(ev);
        $mdDialog.show(confirm).then(function () {
            $scope.ToDoModel.ChildToDos.splice($index, 1);
        }, function () {
            $mdDialog.cancel();
        });
    };
});

