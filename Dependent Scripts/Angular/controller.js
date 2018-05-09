var application = angular.module("application",[]);

var controller = application.controller("SkillsController",function($scope,$http){
    $http.get("https://baalaapi.herokuapp.com/SkillsSelect").then(function(result){
        $scope.SkillsList = result.data;
		
    });
	
	$scope.Style = function(val){
			return { "width" : (val*10) +"%" };
		}
});

var skillsInsert = application.controller("skillsinsertcontroller", function($scope,$http){
	
	$scope.submitfun = function(skill){
	$http.post("https://baalaapi.herokuapp.com/SkillsInsert", skill, {"content-type":"application/json"}).then(function(result){
		alert(result);
	});
	}
});