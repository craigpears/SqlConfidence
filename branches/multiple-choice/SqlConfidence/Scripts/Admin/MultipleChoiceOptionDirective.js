angular.module('sqlConfidenceApp')

    .directive('multipleChoiceOption', function () {
        return {
            restrict: 'E',
            scope: {
                option: '='
            },
            controller: function ($scope) {
            },
            template: "<div>" + 
                "Description: <input ng-model=\"option.Description\" />"+
                "Correct Answer Message: <input ng-model=\"option.CorrectAnswerMessage\" />" +
                "Incorrect Answer Message: <input ng-model=\"option.IncorrectAnswerMessage\" />" +
                "</div>"
        }
    });