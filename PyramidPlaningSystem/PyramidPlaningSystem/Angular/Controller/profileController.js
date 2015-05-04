myApp.controller('profileController', function ($scope, userFactory, assignmentFactory) {
    $scope.todaysDate = new Date();
    $scope.Assignments = [];
    $scope.direction = "left";
    $scope.isCollapsed = false;
    $scope.predicate = '-age';

    getCurrentUser();
    function getCurrentUser() {
        userFactory.getUser().success(function (data) {
            $scope.user = data.Contact.Firstname + " " + data.Contact.Lastname;
            $scope.contactDetails = data.Contact;
            var userId = data.Id;
            getUserAssignments(userId);

        });
    };

    function getUserAssignments(userId) {
        assignmentFactory.getAssignmentsForUser(userId).success(function (data) {
            $.each(data, function(i) {
                $scope.Assignments.push(data[i].Todo);
            });
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

myApp.controller('commentController', function($scope) {
    $scope.isCollapsed = true;
});


