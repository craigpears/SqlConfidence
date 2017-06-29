angular.module('sqlConfidenceApp')

    .directive('unitTestsList', function () {
        return {
            restrict: 'E',
            scope: {
                tests: '='
            },
            controller: function ($scope) {
                $scope.AddTest = function()
                {
                    $scope.tests.push({});
                }
            },
            template: "<span>" +
                "<div ng-repeat=\"test in tests\">" +
                "   Name: <input ng-model=\"test.Name\" />" +
                "   Description: <input ng-model=\"test.Description\" />" +
                "   SQL To Run: <textarea ng-model=\"test.SqlToRun\" />" +
                "   SQL To Compare: <textarea ng-model=\"test.SqlToCompare\" />" +
                "</div>" +
                "<button type=\"button\" ng-click=\"AddTest()\">Add Test</button>" +
                "</span>"
        }
    });