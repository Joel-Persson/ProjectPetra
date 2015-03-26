var myApp = angular.module("myApp", ['ngMaterial', 'ngAnimate']);

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

