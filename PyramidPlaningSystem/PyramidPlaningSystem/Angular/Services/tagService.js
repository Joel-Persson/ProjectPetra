
myApp.service('tagService', function () {
    var tagList = [];
    var childTagList = [];

    var addTags = function (tags) {
        tagList = tags;
    };

    var getTags = function () {
        return tagList;
    }

    var addChildTags = function (tags) {
        childTagList = tags;
    };

    var getChildTags = function () {
        return childTagList;
    }

    return {
        addTags: addTags,
        getTags: getTags,
        addChildTags: addChildTags,
        getChildTags: getChildTags
    };
});