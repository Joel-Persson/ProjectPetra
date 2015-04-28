myApp.controller('profileController', function ($scope, userFactory, assignmentFactory) {
    $scope.todaysDate = new Date();
    $scope.Assignments = [];

    (function getCurrentUser() {
        userFactory.getUser().success(function (data) {
            $scope.user = data.Contact.Firstname + " " + data.Contact.Lastname;
            $scope.contactDetails = data.Contact;
            var userId = data.Id;
            getUserAssignments(userId);

        });
    })();

    function getUserAssignments(userId) {
        assignmentFactory.getAssignmentsForUser(userId).success(function (data) {
            $scope.Assignments = data;
        });
    };

});

myApp.controller('editContactController', function ($scope, contactFactory) {

    $scope.updateContact = function (contactDetails) {
        contactDetails.Id = $scope.contactDetails.Id;
        contactFactory.updateContactDetails(contactDetails).success(function () {
            $scope.status = "Success";
        }).error(function () {
            $scope.status = "Fail";
        });
    };
});


//$scope.format = 'dd EEEE yyyy H:mm:ss';
//myApp.directive("myCurrentTime", function (dateFilter) {
//    return function (scope, element, attrs) {
//        var format;

//        scope.$watch(attrs.myCurrentTime, function (value) {
//            format = value;
//            updateTime();
//        });

//        function updateTime() {
//            var dt = dateFilter(new Date(), format);
//            element.text(dt);
//        }

//        function updateLater() {
//            setTimeout(function () {
//                updateTime(); // update DOM
//                updateLater(); // schedule another update
//            }, 1000);
//        }

//        updateLater();
//    }
//});