myApp.controller('profileController', function ($scope, userFactory, assignmentFactory) {
    $scope.todaysDate = new Date();
    $scope.Assignments = [];
    //$scope.direction = "left";
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

myApp.controller('commentController', function ($scope, commentFactory) {
    $scope.isCollapsed = true;
    var comment = {};
    $scope.comments = [];

    //getComments(toDo.ToDoId);

    $scope.getComments = function (id) {
        commentFactory.getCommentByToDoId(id).success(function (data) {
            $scope.comments = data;
        });
    }

    $scope.submitComment = function (inputComment, item) {
        comment.content = inputComment;
        comment.ToDo = item;
        comment.Author = $scope.user;
        commentFactory.addComment(comment);
        $scope.comments.push(inputComment);
    };
});

myApp.controller('testController', function ($scope, $mdDialog) {

    $scope.centerAnchor = true;
    $scope.namen = { name: "" };

    $scope.toggleCenterAnchor = function () {
        $scope.centerAnchor = !$scope.centerAnchor;
    }

    $scope.draggableObjects = [{ name: 'one', status: "Planed" }, { name: 'two', status: "Planed" }, { name: 'three', status: "Planed" }, { name: 'four', status: "Planed" }];

    $scope.droppedObjects1 = [];
    $scope.droppedObjects2 = [];

    $scope.onDropComplete1 = function (data, evt) {
        var index = $scope.droppedObjects1.indexOf(data);
        if (index == -1) {
            data.status = "Ongoing";
            $scope.droppedObjects1.push(data);
        }

        //för att ta bort från draggable
        var index2 = $scope.draggableObjects.indexOf(data);
        if (index2 > -1) {
            $scope.draggableObjects.splice(index2, 1);
        }
    }

    $scope.onDropComplete2 = function (data, evt) {
        var index = $scope.droppedObjects2.indexOf(data);
        if (index == -1) {
            data.status = "Done";
            $scope.droppedObjects2.push(data);
        }
        //för att ta bort från draggable
        var index2 = $scope.draggableObjects.indexOf(data);
        if (index2 > -1) {
            $scope.draggableObjects.splice(index2, 1);
        }
    }

    $scope.onDragSuccess1 = function (data, evt) {
        console.log("133", "$scope", "onDragSuccess1", "", evt);
        var index = $scope.droppedObjects1.indexOf(data);
        if (index > -1) {
            $scope.droppedObjects1.splice(index, 1);
        }
    }

    $scope.onDragSuccess2 = function (data, evt) {
        var index = $scope.droppedObjects2.indexOf(data);
        if (index > -1) {
            $scope.droppedObjects2.splice(index, 1);
        }
    }

    var inArray = function (array, obj) {
        var index = array.indexOf(obj);
    }

    $scope.addTask = function (task) {
        if (typeof task !== 'undefined') {
            var newTask = {};
            newTask.name = task;
            newTask.status = "Planed";
            $scope.draggableObjects.push(newTask);
        };
        $scope.task = "";
    };

    $scope.removeItem = function (data, listName) {

        switch (listName) {
            case "draggable":
                var index = $scope.draggableObjects.indexOf(data);
                if (index > -1) {
                    $scope.draggableObjects.splice(index, 1);
                }
                break;
            case "dropped1":
                var index2 = $scope.droppedObjects1.indexOf(data);
                if (index2 > -1) {
                    $scope.droppedObjects1.splice(index2, 1);
                }
                break;
            case "dropped2":
                var index3 = $scope.droppedObjects2.indexOf(data);
                if (index3 > -1) {
                    $scope.droppedObjects2.splice(index3, 1);
                }
                break;
            default:
                break;
        }

    }

    $scope.ShowAddSubToDo = function (ev, obj, listName) {
        $mdDialog.show({
            controller: dialogController,
            templateUrl: '/Angular/HtmlTemplates/testEdit.html',
            targetEvent: ev,

        }).then(function (subItem) {
            if (typeof subItem !== 'undefined') {
                switch (listName) {
                    case "draggable":
                        var index = $scope.draggableObjects.indexOf(obj);
                        if (index > -1) {
                            var planedNewTask = {};
                            planedNewTask.name = subItem;
                            planedNewTask.status = "Planed";
                            $scope.draggableObjects[index] = planedNewTask;
                        }
                        break;
                    case "dropped1":
                        var index2 = $scope.droppedObjects1.indexOf(obj);
                        if (index2 > -1) {
                            var ongoingNewTask = {};
                            ongoingNewTask.name = subItem;
                            ongoingNewTask.status = "Ongoing";

                            $scope.droppedObjects1[index2] = ongoingNewTask;
                        }
                        break;
                    case "dropped2":
                        var index3 = $scope.droppedObjects2.indexOf(obj);
                        if (index3 > -1) {
                            var doneNewTask = {};
                            doneNewTask.name = subItem;
                            doneNewTask.status = "Done";
                            $scope.droppedObjects2[index3] = doneNewTask;
                        }
                        break;
                    default:
                        break;
                }

            }
        });
    };


    function dialogController($scope, $mdDialog) {
        $scope.cancel = function () {
            $mdDialog.cancel();
        };
        $scope.addSubItem = function (subItem) {
            $mdDialog.hide(subItem);
        };
    };

});

