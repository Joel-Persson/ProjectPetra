myApp.controller('mainLayoutController', function ($scope, $mdSidenav ) {

    $scope.toggleSidenav = function (menuId) {
        $mdSidenav(menuId).toggle();
    }

    $scope.close = function() {
        $mdSidenav('right').close();
    };

    $scope.showAndHideBool = true;
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


