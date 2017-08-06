var application = angular.module("application",[]);

application.controller("AdminUploadController",function($scope,$http){
    $scope.UploadData = { name : "" , type : "", image:"", enabled:"" };
    $scope.submit = function(){
        var formdata = new FormData();
        formdata.append("name", $scope.UploadData.name);
        formdata.append("type", $scope.UploadData.type);
        formdata.append("image", document.querySelector("#image").files[0]);
        formdata.append("enabled", $scope.UploadData.enabled);
        $http.post("http://localhost:99/siteapi/Skills/InsertSkill", formdata, {headers:{'Content-Type': undefined}});
        $scope.UploadData = { name : "" , type : "", image:"", enabled:"" };
    }
});