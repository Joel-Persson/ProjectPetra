var myApp = angular.module("myApp", ['ngMaterial', 'ngAnimate', 'ngRoute', 'ngMessages', 'ui.bootstrap']);

myApp.directive("subitems", function () {
    return {
        restrict: "A",
        templateUrl: "/Angular/Directives/subItems.html"
    };
});


myApp.controller('leftController', function ($scope, $mdSidenav) {

    $scope.close = function () {
        $mdSidenav('left').close();
    }

});



