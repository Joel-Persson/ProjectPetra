var myApp = angular.module("myApp", ['ngMaterial']);

myApp.controller('mainLayoutController', function ($scope, $mdSidenav) {
    $scope.toggleSidenav = function(menuId) {
        $mdSidenav(menuId).toggle();
    }
});