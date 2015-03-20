var myApp = angular.module("myApp", ['ngMaterial']);


myApp.config(function($mdThemingProvider) {
    $mdThemingProvider.definePalette('amazingPaletteName', {
        '50': '55BA47', //green
        '100': 'E0E0E0', // grey
        '200': 'CF118C',//pink
        '300': '702C91',//purple
        '400': 'ffffff', //white
        '500': 'f44336',
        '600': 'BDBDBD',// hover (darkergrey)
        '700': 'd32f2f',
        '800': 'c62828',
        '900': 'b71c1c',
        'A100': 'ff8a80',
        'A200': 'ff5252',
        'A400': 'ff1744',
        'A700': 'd50000',
        'contrastDefaultColor': 'light',    // whether, by default, text (contrast)
        // on this palette should be dark or light
        'contrastDarkColors': ['50', '100', //hues which contrast should be 'dark' by default
         '200', '300', '400', 'A100'],
        'contrastLightColors': undefined    // could also specify this if default was 'dark'
    });
    $mdThemingProvider.theme('default')
        .primaryPalette('amazingPaletteName', {
            'default': '50', // by default use shade 400 from the pink palette for primary intentions
            'hue-1': '100', // use shade 100 for the <code>md-hue-1</code> class
            'hue-2': '200', // use shade 600 for the <code>md-hue-2</code> class
            'hue-3': '300' // use shade A100 for the <code>md-hue-3</code> class
            
        });
});


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
});


myApp.controller('leftController', function($scope, $mdSidenav) {
    $scope.close = function() {
        $mdSidenav('left').close();
    }
});

