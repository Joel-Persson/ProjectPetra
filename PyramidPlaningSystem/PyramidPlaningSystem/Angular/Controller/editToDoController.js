myApp.controller('editToDoController', function ($scope, $routeParams, toDoFactory, $location, $mdDialog, formatDateFactory, tagService, convertService, assignmentFactory) {
    $scope.PageHeader = "Edit Task";
    $scope.oneAtATime = true;
    $scope.Assignments = [];

    getSingleToDo();
    function getSingleToDo() {
        toDoFactory.getSingleToDo($routeParams.Id).success(function (data) {
            $scope.toDoModel = data;
            $scope.deadlineDate = new Date(data.ParentToDo.ToDo.Deadline);
            $scope.startDate = new Date(data.ParentToDo.ToDo.StartDate);
            $scope.endDate = new Date(data.ParentToDo.ToDo.EndDate);
            getAssignments(data.ParentToDo.ToDo.ToDoId);
            })
        .error(function () {
            $scope.status = "Something went wrong!";
        });

    };

    function getAssignments(toDoId) {
        assignmentFactory.getAssignments(toDoId).success(function (data) {
            $.each(data, function(i) {
                $scope.Assignments.push(data[i].User.Contact.Firstname + " " + data[i].User.Contact.Lastname);
            });
        });
    }

    //$.each($scope.FullContacts, function (x) {
    //    if (tagList[i] === $scope.FullContacts[x].Name) {
    //        contactIdList.push($scope.FullContacts[x].Id);
    //    }
    //});

    $scope.editToDo = function (toDoModel) {

        toDoModel.ToDo.Deadline = $scope.deadlineDate;
        toDoModel.ToDo.StartDate = $scope.startDate;
        toDoModel.ToDo.EndDate = $scope.endDate;

        formatDateFactory.formatTime(toDoModel.ToDo);
        toDoModel.ContactIdList = tagService.getTags();
        toDoFactory.editToDo(toDoModel).success(function () {
            $location.path('/toDos');
        });
    };



    $scope.ShowAddSubToDo = function (ev) {
        $mdDialog.show({
            controller: DialogController,
            templateUrl: '/Angular/HtmlTemplates/addSubToDo.html',
            targetEvent: ev,

        }).then(function (subItem) {
            subItem.ToDo = convertService.convertTodo(subItem);
            subItem.ContactIdList = tagService.getChildTags();
            $scope.toDoModel.ChildToDos.push(subItem);
            $scope.editSubToDo($scope.toDoModel);
        });
    };

    function DialogController($scope, $mdDialog) {

        $scope.cancel = function () {
            $mdDialog.cancel();
        };
        $scope.addSubItem = function (subItem) {
            $mdDialog.hide(subItem);
        };
    };

    $scope.deleteConfirm = function (ev, toDo) {
        var confirm = $mdDialog.confirm()
          .title('Confirm')
          .content('Are you sure you want to delete the ToDo?')
          .ok('Yes')
          .cancel('No')
          .targetEvent(ev);
        $mdDialog.show(confirm).then(function () {
            $scope.toDoModel.ChildToDos.splice($scope.toDoModel.ChildToDos.indexOf(toDo), 1);
            $scope.deleteToDo(toDo.ToDoId);

        }, function () {
            $mdDialog.cancel();
        });
    };

    $scope.deleteToDo = function (toDoId) {
        toDoFactory.deleteToDo(toDoId).success(function () {
            $scope.status = "Deleted";
        }).error(function () {
            $scope.status = "Error";
        });
    };

    $scope.editSubToDo = function (toDoModel) {
        formatDateFactory.formatTime(toDoModel);

        toDoFactory.addToDo(toDoModel).success(function () {
            $scope.success = "Yes";
        })
            .error(function () {
                $scope.success = "No";
            });

    };

    $scope.status = {
        isFirstOpen: true,
        isFirstDisabled: false
    };
});

myApp.controller('AccordionDemoCtrl', function ($scope) {
    $scope.oneAtATime = true;

    $scope.groups = [
      {
          title: 'Dynamic Group Header - 1',
          content: 'Dynamic Group Body - 1'
      },
      {
          title: 'Dynamic Group Header - 2',
          content: 'Dynamic Group Body - 2'
      }
    ];

    $scope.items = ['Item 1', 'Item 2', 'Item 3'];

    $scope.addItem = function () {
        var newItemNo = $scope.items.length + 1;
        $scope.items.push('Item ' + newItemNo);
    };

    $scope.status = {
        isFirstOpen: true,
        isFirstDisabled: false
    };
});
