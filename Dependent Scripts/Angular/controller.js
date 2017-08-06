var application = angular.module("application",[]);

var controller = application.controller("SkillsController",function($scope,$http){
    $http.get("http://192.168.0.113:99/siteapi/skills").then(function(result){
        $scope.ImageObjectList = result.data;
    });
});