var sqlConfidenceApp = angular.module('sqlConfidenceApp', ['ngSanitize']);

sqlConfidenceApp.controller('AdminQuestionCtrl', function ($scope, bootstrappedData, $http, $q) {
    $scope.data = bootstrappedData.data;
    $scope.exerciseHref = "/Admin/Exercise/?ExerciseId=" + $scope.data.ExerciseId;
    $scope.SaveQuestion = function()
    {
        $http.post("/Admin/SaveQuestion", $scope.data).then(function (response) {
            if (response.data.Success) {
                ShowSuccessMessage("Question saved successfully");
                $scope.data = eval(response.data.Data);
            }
            else {
                ShowErrorMessage(response.ErrorMessage);
            }
        });
    }
});