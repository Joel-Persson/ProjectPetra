myApp.config(function ($routeProvider) {
    $routeProvider
           .when('/',
        {
            controller: 'addToDoController',
            templateUrl: '/Angular/HtmlTemplates/AddToDo.html'
        })
              .when('/toDos',
        {
            controller: 'addToDoController',
            templateUrl: '/Angular/HtmlTemplates/toDos.html'
        })
        .otherwise({
            redirectTo: '/'
        });
});