
myApp.controller('addToDoController', function ($scope, toDoFactory, $mdDialog, $location, formatDateFactory, tagService) {

    $scope.PageHeader = "Add Task";
    $scope.Time = "";
    $scope.ToDoModel = {
        ParentToDo: {},
        ChildToDos: []
    };

    $scope.add = function (ToDoModel) {

        formatDateFactory.formatTime(ToDoModel);

        toDoFactory.addToDo(ToDoModel).success(function () {
            $scope.success = "Yes";
            $location.path('/toDos');
        })
            .error(function () {
                $scope.success = "No";
            });

    };

    $scope.ShowAddSubToDo = function (ev) {
        $mdDialog.show({
            controller: DialogController,
            templateUrl: '/Angular/HtmlTemplates/addSubToDo.html',
            targetEvent: ev,

        }).then(function (subItem) {
            $scope.ToDoModel.ChildToDos.push(subItem);

        });
    };

    $scope.showConfirm = function (ev, subItem) {
        var confirm = $mdDialog.confirm()
          .title('Confirm')
          .content('Are you sure you want to delete the sub task?')
          .ok('Yes')
          .cancel('No')
          .targetEvent(ev);
        $mdDialog.show(confirm).then(function () {
            $scope.ToDoModel.ChildToDos.splice($scope.ToDoModel.ChildToDos.indexOf(subItem), 1);
        }, function () {
            $mdDialog.cancel();
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

});

myApp.controller('assignmentController', function ($scope, contactFactory, tagService) {

    $scope.contacts = [];

    $scope.FullContacts = [];

    $scope.tags = [];

    (function getContacts() {
        contactFactory.getContacts().success(function (data) {
            //$scope.contacts = data;

            $.each(data, function (i) {
                var contactObject = {
                    "Name": data[i].Firstname + " " + data[i].Lastname,
                    "Id": data[i].Id
                }
                $scope.FullContacts.push(contactObject);
                $scope.contacts.push(data[i].Firstname + " " + data[i].Lastname);
            });
        });
    })();



    $scope.$watch('tags', function (tag) {
        tagService.addTags(tag);
        $scope.test = tagService.getTags();
    }, true);

});
