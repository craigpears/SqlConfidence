sqlConfidenceApp.controller('MultipleChoiceCtrl', function ($scope, $http, $filter, $timeout, responseParserService, jsonDeserializerService) {
    $scope.SelectQuestion = function (question) {
        $scope.currentQuestion = question;        
        $scope.ShowReferenceData();
    }

    $scope.CheckAnswerMultipleChoice = function (answer, questionId)
    {
        if (answer.toLowerCase() === $scope.currentQuestion.AnswerTemplate.toLowerCase())
        {
            $http.post("/Exercise/LogCorrectAnswer?QuestionId=" + questionId).then(function (response) {
                response.data.Success ? $scope.CorrectAnswer() :  ShowErrorMessage("There was a problem submitting your answer");
            });
        }
        else
        {
            ShowErrorMessage("Sorry, that wasn't correct");
        }
    }
    
    $scope.CorrectAnswer = function()
    {
        $scope.currentQuestion.Answered = true;
        ShowSuccessMessage("<strong>Correct</strong>");
    }

    $scope.ShowReferenceData = function () {
        var questionId = $scope.currentQuestion.ExerciseQuestionId;
        var dataQueries = $scope.currentQuestion.DataQueries;
        $scope.headers = [];
        $scope.rows = [];
        for(var i = 0; i < dataQueries.length; i++)
        {
            $http.post("/Exercise/ExecuteUserQueryJson", { QuestionId: questionId, Query: dataQueries[i].SqlQuery }).then(function (response) {
                $scope.RenderTableFromResponse(response);
            });
        }
    };

    $scope.NextQuestion = function () {
        // Get the next unanswered question
        var unansweredQuestions = $filter('unansweredQuestions')($scope.questions);
        if (unansweredQuestions.length === 0) {
            ShowSuccessMessage("You answered all the questions correctly", -1);
        }
        else {
            // Sort the unanswered questions.  Prioritise the questions further down the list from the current question, then return them in order
            unansweredQuestions.sort(function (a, b) { (a.Order + (a.Order > $scope.currentQuestion ? 100 : 0)) - (b.Order + (b.Order > $scope.currentQuestion ? 100 : 0)) });
            $scope.SelectQuestion(unansweredQuestions[0]);
        }
    }

    $scope.RenderTableFromResponse = function(response)
    {
        var returnObj = responseParserService.parseResponse(response);
        $scope.headers.push(returnObj.headers);
        $scope.rows.push(returnObj.rows);
    }

    $scope.GetInstructionsTitle = function () {
        if ($scope.currentQuestion == null) return "Loading...";
        var questionNumber = $scope.currentQuestion.Order + 1;
        var questionDescription = $scope.currentQuestion.Description;
        return questionNumber + ". " + questionDescription;
    }

    $scope.init = function () {
        $http.get("/api/MultipleChoice/Get/" + ExerciseId).then(function (response) {
            var responseData = response.data;
            jsonDeserializerService.retrocycle(responseData);
            $scope.exercise = responseData;
            $scope.questions = $scope.exercise.Questions;
            $scope.NextQuestion();
        });        
    }
    
    $scope.init();
});