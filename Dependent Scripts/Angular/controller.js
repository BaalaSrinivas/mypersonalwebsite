var application = angular.module("application",[]);

var controller = application.controller("SkillsController",function($scope,$http){
    $http.get("http://localhost:99/siteapi/skills").then(function(result){
        $scope.ImageObjectList = result.data;
    });
});