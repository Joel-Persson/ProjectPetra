var mainApp = angular.module('mainApp', ['ngRoute']);


mainApp.config(function ($routeProvider) {
    $routeProvider
         .when("/index",
         {
             controller: 'ContactInfoController',
             templateUrl: '/Index.html'
         })
        .otherwise({
            redirectTo: '/'
        });
});

//mainApp.factory('contactFactory', function($http) {

//    var urlBase = "/api/contact";
//    var contactFactory = {};

//    contactFactory.addContactInfo = function(contactInfo) {
//        return $http.post(urlBase, contactInfo);
//    }

//    return contactFactory;
//});

mainApp.controller('ContactInfoController', function ($scope, contactFactory, $location) {
    
    //$scope.addContact = function() {
    //    contactFactory.addContactInfo(this.contactInfo).success(function() {
    //        $scope.status = "Success";
    //        $location.path("../Partials/Index.html");
    //        })
    //        .error(function() {
    //            $scope.status = "Fail";
    //        });
    //}

});