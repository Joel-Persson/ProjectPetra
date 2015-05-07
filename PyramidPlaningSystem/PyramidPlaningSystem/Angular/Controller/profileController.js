﻿myApp.controller('profileController', function ($scope, userFactory, assignmentFactory) {
    $scope.todaysDate = new Date();
    $scope.Assignments = [];
    $scope.direction = "left";
    $scope.isCollapsed = false;
    $scope.predicate = '-age';
    var toDo = {};

    getCurrentUser();
    function getCurrentUser() {
        userFactory.getUser().success(function (data) {
            $scope.user = data.Contact.Firstname + " " + data.Contact.Lastname;
            $scope.contactDetails = data.Contact;
            toDo = data.ToDo;
            var userId = data.Id;
            getUserAssignments(userId);

        });
    };

    function getUserAssignments(userId) {
        assignmentFactory.getAssignmentsForUser(userId).success(function (data) {
            $.each(data, function (i) {
                var tempData = {
                    "ToDo": data[i].Assignment.Todo,
                    "Comments": data[i].Comments
            };

                $scope.Assignments.push(tempData);
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

myApp.controller('commentController', function ($scope, commentFactory) {
    $scope.isCollapsed = true;
    var comment = {};
    $scope.comments = [];

    //getComments(toDo.ToDoId);

    $scope.getComments = function(id) {
        commentFactory.getCommentByToDoId(id).success(function (data) {
            $scope.comments = data;
        });
    }

    $scope.submitComment = function (inputComment, item) {
        comment.Content = inputComment;
        comment.ToDo = item.ToDo;
        comment.Author = $scope.user;
        commentFactory.addComment(comment);
        item.Comments.push(comment);
        $scope.comment = "";
    };
});


