var sqlConfidenceApp = angular.module('sqlConfidenceApp', ['ngSanitize']);

sqlConfidenceApp.controller('MultipleChoiceCtrl', function ($scope, $http) {
    $scope.exercise = {};
    $scope.Save = function()
    {
        $http.post("/Admin/NewMultipleChoiceExercise", $scope.exercise).then(function (response) {
            if (response.data.Success) {
                ShowSuccessMessage("Exercise saved successfully");
                $scope.exercise = eval(response.data.Data);
            }
            else {
                ShowErrorMessage(response.ErrorMessage);
            }
        });
    }
});