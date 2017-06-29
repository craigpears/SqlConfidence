sqlConfidenceApp.directive("unitTestsRunner", function () {
    return {
        restrict: 'E',
        scope: {
            tests: '='
        },
        templateUrl: '/Views/Directives/UnitTestsRunner.html'
    }
});