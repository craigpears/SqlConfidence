angular.module('sqlConfidenceApp')

    .directive('multipleChoiceInput', function () {
        return {
            restrict: 'E',
            scope: {
                answer: '=',
                choices: '='
            },
            controller: function ($scope) {
                $scope.AddChoice = function()
                {
                    if ($scope.choices == null)
                    {
                        $scope.choices = [];
                    }
                    $scope.choices.push({
                        Description: $scope.newChoiceDescription
                    });
                }
            },
            template: "<span>" +
                "<div ng-repeat=\"choice in choices\">" +
                "   <input ng-model=\"choice.Description\" />" +
                "   <input type=\"radio\" ng-model=\"$parent.answer\" value=\"{{choice.Description}}\" name=\"CorrectAnswer\" />" +
                "</div>" +
                "<input ng-model=\"newChoiceDescription\" /> <button type=\"button\" ng-click=\"AddChoice()\">Add Choice</button>" +
                "</span>"
        }
    });