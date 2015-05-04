
myApp.controller('addToDoController', function ($scope, toDoFactory, $mdDialog, $location, formatDateService, tagService, convertService) {

    $scope.PageHeader = "Add Task";
    //$scope.EditAssignments = [];
    $scope.Time = "";
    $scope.ToDoModel = {
        ParentToDo: {},
        ChildToDos: []
    };
    $scope.controllerTest = "Från edit till assignment";

    $scope.add = function (ToDoModel) {
        modifyModel(ToDoModel);
        $.each(ToDoModel.ChildToDos, function (i) {
            ToDoModel.ChildToDos[i].ToDo = convertService.convertTodo(ToDoModel.ChildToDos[i]);
        });
        toDoFactory.addToDo(ToDoModel).success(function () {
            $location.path('/toDos');
        })
            .error(function () {
                $scope.success = "No";
            });
    };




    $scope.ShowAddSubToDo = function (ev) {
        $mdDialog.show({
            controller: dialogController,
            templateUrl: '/Angular/HtmlTemplates/addSubToDo.html',
            targetEvent: ev,

        }).then(function (subItem) {
            subItem.ContactIdList = tagService.getChildTags();
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

    function dialogController($scope, $mdDialog) {
        $scope.cancel = function () {
            $mdDialog.cancel();
        };
        $scope.addSubItem = function (subItem) {
            $mdDialog.hide(subItem);
        };
    };

    function modifyModel(ToDoModel) {
        formatDateService.formatTime(ToDoModel);
        ToDoModel.ParentToDo.ToDo = convertService.convertTodo(ToDoModel.ParentToDo);
        ToDoModel.ParentToDo.ContactIdList = tagService.getTags();
    };

});

myApp.controller('assignmentController', function ($scope, contactFactory, tagService) {

    $scope.contacts = [];

    $scope.FullContacts = [];

    $scope.tags = [];


    (function getContacts() { 
        contactFactory.getContacts().success(function (data) {

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



    $scope.$watch('tags', function (tagList) {
        var contactIdList = [];
        $.each(tagList, function (i) { // ändra så man slipper ha två foreach loopar
            $.each($scope.FullContacts, function (x) {
                if (tagList[i] === $scope.FullContacts[x].Name) {
                    contactIdList.push($scope.FullContacts[x].Id);
                }
            });
        });

        tagService.addTags(contactIdList);
    }, true);


});




myApp.controller('subAssignmentController', function ($scope, contactFactory, tagService) {

    $scope.contacts = [];

    $scope.FullContacts = [];

    $scope.tags = [];

    (function getContacts() {
        contactFactory.getContacts().success(function (data) {

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



    $scope.$watch('tags', function (tagList) {
        var contactIdList = [];
        $.each(tagList, function (i) { // ändra så man slipper ha två foreach loopar
            $.each($scope.FullContacts, function (x) {
                if (tagList[i] === $scope.FullContacts[x].Name) {
                    contactIdList.push($scope.FullContacts[x].Id);
                }
            });
        });

        tagService.addChildTags(contactIdList);
        $scope.test = tagService.getChildTags();
    }, true);


});
