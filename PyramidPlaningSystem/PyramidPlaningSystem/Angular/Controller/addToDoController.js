﻿
myApp.controller('addToDoController', function ($scope, toDoFactory, $mdDialog, $location, formatDateFactory, tagService, assignmentFactory) {

    $scope.PageHeader = "Add Task";
    $scope.Time = "";
    $scope.ToDoModel = {
        ParentToDo: {},
        ChildToDos: []
    };

    $scope.add = function (ToDoModel) {

        formatDateFactory.formatTime(ToDoModel);

        function Convert(ToDoModel) {
            var ToDo = {};
            ToDo.Title = ToDoModel.Title;
            ToDo.Description = ToDoModel.Description;
            ToDo.Effort = ToDoModel.Effort;
            ToDo.Deadline = ToDoModel.Deadline;
            ToDo.EndDate = ToDoModel.EndDate;
            ToDo.StartDate = ToDoModel.StartDate;
            ToDo.Priority = ToDoModel.Priority;
            return ToDo;
        };

        ToDoModel.ParentToDo.ToDo = Convert(ToDoModel.ParentToDo);
        ToDoModel.ParentToDo.UniqueId = tagService.replace();
        ToDoModel.ParentToDo.ContactIdList = tagService.getTags();

        $.each(ToDoModel.ChildToDos, function(i) {
            ToDoModel.ChildToDos[i].ToDo = Convert(ToDoModel.ChildToDos[i]);
            ToDoModel.ChildToDos[i].UniqueId = tagService.replace();
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
            controller: DialogController,
            templateUrl: '/Angular/HtmlTemplates/addSubToDo.html',
            targetEvent: ev,

        }).then(function (subItem) {
            subItem.ContactIdList = tagService.getChildTags();
            $scope.ToDoModel.ChildToDos.push(subItem);//kanske skapa ett unikt id för varje child här

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
        $.each(tagList, function(i) { // ändra så man slipper ha två foreach loopar
            $.each($scope.FullContacts, function (x) {
                if (tagList[i] === $scope.FullContacts[x].Name) {
                    contactIdList.push($scope.FullContacts[x].Id);
                }
            });
        });

    tagService.addTags(contactIdList);
        $scope.test = tagService.getTags();
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
