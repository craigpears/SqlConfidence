sqlConfidenceApp.directive("queryResultsView", function () {
    return {
        restrict: 'E',
        scope: {
            headers: '=', // An array of headers for the table.  ['First Name', 'Second Name', 'Gender']
            rows: '=' // The array of data to go into the table.  [['Adam', 'Smith', 'Male'],['Alice','Johnson','Female']]
        },
        templateUrl: '/Views/Directives/QueryResultsView.html'
    }
});