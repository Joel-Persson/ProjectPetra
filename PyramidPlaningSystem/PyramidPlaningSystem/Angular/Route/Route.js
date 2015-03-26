myApp.config(function ($routeProvider) {
    $routeProvider
           .when('/',
        {
            controller: 'addToDoController',
            templateUrl: '/Angular/HtmlTemplates/AddToDo.html'
        })
        .otherwise({
            redirectTo: '/'
        });
});