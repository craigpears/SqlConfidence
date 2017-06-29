sqlConfidenceApp.directive("collapsible", function () {
    return {
        restrict: 'A',
        link: function (scope, element, attrs) {
            scope.isCollapsed = true;
        }
    }
});