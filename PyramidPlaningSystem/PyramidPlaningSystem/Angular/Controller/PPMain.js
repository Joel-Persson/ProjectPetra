var myApp = angular.module("myApp", ['ngMaterial', 'ngAnimate', 'ngRoute']);

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


myApp.controller('leftController', function($scope, $mdSidenav) {

    $scope.close = function () {
        $mdSidenav('left').close();
    }

});


myApp.controller('addToDoController', function($scope, toDoFactory) {

    $scope.add = function () {
        toDoFactory.addToDo(this.toDo).success(function() {
                $scope.success = "Yes";
            })
            .error(function() {
                $scope.success = "No";
            });
    }

});

