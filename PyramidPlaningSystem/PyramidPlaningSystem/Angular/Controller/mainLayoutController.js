myApp.controller('mainLayoutController', function ($scope, $mdSidenav) {

    $scope.toggleSidenav = function (menuId) {
        $mdSidenav(menuId).toggle();
    }

    $scope.getWidth = function () {
        return $(window).width();
    };
    $scope.$watch($scope.getWidth, function (newValue) {
        $scope.windowWidth = newValue;

    });
    window.onresize = function () {
        $scope.$apply();
    }

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