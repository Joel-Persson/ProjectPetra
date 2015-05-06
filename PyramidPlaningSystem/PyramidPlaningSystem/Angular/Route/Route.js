myApp.config(function ($routeProvider) {
    $routeProvider
           .when('/',
            {
                controller: 'addToDoController',
                templateUrl: '/Angular/HtmlTemplates/AddToDo.html'
            })
           .when('/addToDo',
            {
                controller: 'addToDoController',
                templateUrl: '/Angular/HtmlTemplates/AddToDo.html'
            })
            .when('/test',
            {
                controller: 'testController',
                templateUrl: '/Angular/HtmlTemplates/test.html'
            })
              .when('/toDos',
            {
                controller: 'listAllToDosController',
                templateUrl: '/Angular/HtmlTemplates/listAllToDos.html'
            })
               .when('/editToDo/:Id',
            {
                controller: 'editToDoController',
                templateUrl: '/Angular/HtmlTemplates/editToDo.html'
            })
            .when('/profile',
            {
                controller: 'profileController',
                templateUrl: '/Angular/HtmlTemplates/profilePage.html'
            })
                .otherwise({
                    redirectTo: '/'
                });
});