myApp.controller('editToDoController', function ($scope, $routeParams, toDoFactory, $location, $mdDialog, formatDateFactory, tagService, convertService, assignmentFactory) {
    $scope.PageHeader = "Edit Task";
    $scope.oneAtATime = true;
    $scope.Assignments = [];

    getSingleToDo();
    function getSingleToDo() {
        toDoFactory.getSingleToDo($routeParams.Id).success(function (data) {
            $scope.toDoModel = data;

            $scope.toDoModel.ParentToDo.ToDo.Deadline = new Date($scope.toDoModel.ParentToDo.ToDo.Deadline);
            $scope.toDoModel.ParentToDo.ToDo.StartDate = new Date($scope.toDoModel.ParentToDo.ToDo.StartDate);
            $scope.toDoModel.ParentToDo.ToDo.EndDate = new Date($scope.toDoModel.ParentToDo.ToDo.EndDate);
           
            $.each($scope.toDoModel.ChildToDos, function (i) {
                $scope.toDoModel.ChildToDos[i].ToDo.Deadline = new Date($scope.toDoModel.ChildToDos[i].ToDo.Deadline);
                $scope.toDoModel.ChildToDos[i].ToDo.StartDate = new Date($scope.toDoModel.ChildToDos[i].ToDo.StartDate);
                $scope.toDoModel.ChildToDos[i].ToDo.EndDate = new Date($scope.toDoModel.ChildToDos[i].ToDo.EndDate);
            });
            getAssignments(data);
            })
        .error(function () {
            $scope.status = "Something went wrong!";
        });

    };

    function getAssignments(toDoModel) {
        assignmentFactory.getAssignments(toDoModel.ParentToDo.ToDo.ToDoId).success(function (parentAssignment) {
            $.each(parentAssignment, function (i) {
                $scope.Assignments.push(parentAssignment[i].User.Contact.Firstname + " " + parentAssignment[i].User.Contact.Lastname);
            });

        });
    }


    $scope.editToDo = function (toDoModel) {

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

    $scope.editSubToDo = function (toDoModel) {
        formatDateFactory.formatTime(toDoModel);

        toDoFactory.addToDo(toDoModel).success(function () {
            $scope.success = "Yes";
        })
            .error(function () {
                $scope.success = "No";
            });

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
