var myApp = angular.module("myApp",
    ['ngMaterial',
        'ngAnimate',
        'ngRoute',
        'ngMessages',
        'ui.bootstrap.collapse',
        'tagger',
        'angular-timeline']);

myApp.directive("subitems", function () {
    return {
        restrict: "A",
        templateUrl: "/Angular/Directives/subItems.html"
    };
});




