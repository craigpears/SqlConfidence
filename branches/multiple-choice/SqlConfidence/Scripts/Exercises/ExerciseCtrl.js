sqlConfidenceApp.controller('ExerciseCtrl', function ($scope, $http, $filter, $timeout, boostrappedExerciseData, bootstrappedUserData, responseParserService) {
    $scope.ChangeQueryView = function (tab) { $scope.tab = tab;}

    $scope.SelectQuestion = function (question) {
        $scope.HideHintAndSolution();
        $scope.currentQuestion = question;
        if (question.StartingSql !== null && question.StartingSql.trim() !== "") {
            codeMirror.setValue(question.StartingSql);
        }
        
        if (bootstrappedUserData.user.IsAdmin) {
            CKEDITOR.instances["current-instruction"].setData($scope.GetInstructions());
        }

        $scope.ShowAnswerPreview(question.ExerciseQuestionId);
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

    $scope.RunTests = function(question)
    {
        var testsPassed = 0;

        angular.forEach(question.ExerciseQuestionUnitTests, function (test) {
            $http.post("/Exercise/RunUnitTest?QuestionId=" + question.ExerciseQuestionId + "&UnitTestId=" + test.ExerciseQuestionUnitTestId).then(function (response) {
                if (response.data.Success) {
                    test.Passed = response.data.Passed;
                    if (test.Passed) testsPassed++;
                    if (testsPassed === question.ExerciseQuestionUnitTests.length) {
                        $http.post("/Exercise/LogCorrectAnswer?QuestionId=" + question.ExerciseQuestionId).then(function (response) {
                            if (response.data.Success) {
                                $scope.CorrectAnswer();
                            }
                            else {
                                ShowErrorMessage("There was a problem logging your answer as correct");
                            }
                        });
                    }
                }
                else {
                    ShowErrorMessage("There was a problem running a unit test.  Make sure you are logged in before doing any unit test exercises");
                }
            });
        });


        $scope.ChangeRightTab('UnitTests');
    }

    $scope.Reset = function(question)
    {
        if (confirm("This will undo all your queries for this question, are you sure?"))
        {
            $http.post("/Exercise/Reset?QuestionId=" + question.ExerciseQuestionId).then(function (response) {
                response.data.Success ? ShowSuccessMessage("Successfully reset the data") : ShowErrorMessage("There was a problem resetting the data");
            });
        }
    }

    $scope.CheckAnswer = function (questionId) {
        $http.post("/Exercise/CheckAnswer", { QuestionId: questionId, Query: $scope.GetUserQuery() }).then(function (response) {
            $scope.headers = null;
            $scope.rows = null;
            $("#QueryResults").html("");

            if (response.data.error !== undefined) {
                $("#QueryResults").html("<p class=\"error-message\">" + response.data.error + "</p>");
            }
            else if (response.data.correctAnswer) {
                $scope.CorrectAnswer();
            }
            else {
                $("#QueryResults").html(response.data.differences);
            }
        });
    }

    $scope.CorrectAnswer = function()
    {
        $scope.currentQuestion.Answered = true;
        ShowSuccessMessage("<strong>Correct</strong>");
    }

    $scope.ExecuteQuery = function () {
        var questionId = $scope.currentQuestion.ExerciseQuestionId;
        $http.post("/Exercise/ExecuteUserQueryJson", { QuestionId: questionId, Query: $scope.GetUserQuery() }).then(function (response) {
            $scope.showingPreview = false;
            $scope.RenderTableFromResponse(response);
        });
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

    $scope.ShowAnswerPreview = function () {
        if ($scope.QuestionIsQueryType()) {
            var answerTemplate = $scope.currentQuestion.AnswerTemplate;
            var url = "/Exercise/ExecuteQueryJson?"
            url += "QuestionId=" + $scope.currentQuestion.ExerciseQuestionId;
            url += "&Query=" + encodeURIComponent(answerTemplate);
            url += "&UserId=-1";
            $http.get(url).then(function (response) {
                $scope.RenderTableFromResponse(response);
                $scope.showingPreview = true;
            });
        }
    }

    $scope.RenderTableFromResponse = function (response) {
        var returnObj = responseParserService.parseResponse(response);
        $scope.headers = returnObj.headers;
        $scope.rows = returnObj.rows;
    }

    $scope.GetUserQuery = function() {
        var query = codeMirror.getValue();

        // Build the query from the query builder if they are on that tab
        if ($scope.tab === 'Query Builder') {
            query = $scope.queryBuilderControl.GetQueryFromBuilder();
        }

        return query;
    }

    $scope.ChangeRightTab = function(tab)
    {
        $scope.rightTab = tab;
    }

    $scope.ToggleShowHint = function () {
        $scope.showHint = !$scope.showHint;
    }

    $scope.ToggleShowSolution = function () {
        $scope.showSolution = !$scope.showSolution;
    }

    $scope.HideHintAndSolution = function () {
        $scope.showHint = false;
        $scope.showSolution = false;
    }

    $scope.HidePreview = function () {
        $scope.showingPreview = false;
    }

    $scope.QuestionIsQueryType = function () {
        return $scope.currentQuestion.ExerciseQuestionType === 0;
    }

    $scope.QuestionIsMultipleChoice = function () {
        return $scope.currentQuestion.ExerciseQuestionType === 1;
    }

    $scope.QuestionIsUnitTested = function () { 
        return $scope.currentQuestion.ExerciseQuestionType === 2;
    }

    $scope.GetHint = function () {
        return $scope.currentQuestion.Hint;
    }

    $scope.GetSolution = function () {
        return $scope.currentQuestion.AnswerTemplate;
    }

    $scope.GetInstructions = function () {
        return $scope.currentQuestion.InstructionsTemplate;
    }

    $scope.GetInstructionsTitle = function () {
        var questionNumber = $scope.currentQuestion.Order + 1;
        var questionDescription = $scope.currentQuestion.Description;
        return questionNumber + ". " + questionDescription;
    }

    $scope.GetTableForQueryBuilder = function () {
        return $scope.exercise.DataSource.DataSourceTables[0];
    }

    $scope.queryBuilderControl = {};

    // Look for any inline edit controls and wire the edits up to auto save events for the whole current question
    $scope.autoSavePromise = null;
    $scope.autoSaveQuestion = function () {
        $scope.currentQuestion.InstructionsTemplate = CKEDITOR.instances["current-instruction"].getData();
        if ($scope.autoSavePromise !== null) $timeout.cancel($scope.autoSavePromise);
        $scope.autoSavePromise = $timeout($scope.autoSaveQuestionPost, 3000);
    }

    $scope.autoSaveQuestionPost = function () {
        $scope.autoSavePromise = null;
        $http.post("/Admin/SaveQuestion", $scope.currentQuestion).then(function (response) {
            response.data.Success ? ShowSuccessMessage("Data Saved Successfully") : ShowErrorMessage("Error saving data");
        });
    }

    //==== INIT ====//
    codeMirror = CodeMirror.fromTextArea(document.getElementById("QueryText"),
                {
                    lineNumbers: true,
                    matchBrackets: true,
                    indentUnit: 4,
                    mode: "text/x-mssql",
                    extraKeys: { "Ctrl-Space": "autocomplete" },
                    lineWrapping: true
                });

    if (bootstrappedUserData.user.IsAdmin) {
        document.getElementById("current-instruction").contentEditable = true;
        CKEDITOR.inline('current-instruction');
        CKEDITOR.instances["current-instruction"].on("change", $scope.autoSaveQuestion);
    }

    $scope.exercise = boostrappedExerciseData.exercise;
    $scope.questions = $scope.exercise.ExerciseQuestions;
    $scope.NextQuestion();
    $scope.showingPreview = false;
    $scope.tab = $scope.exercise.ShowQueryBuilder ? 'Query Builder' : 'SQL';
    $scope.rightTab = 'DatabaseSchema';
    $scope.HideHintAndSolution();

    for (var i = 0; i < $scope.exercise.DataSource.DataSourceTables.length; i++)
    {
        $scope.exercise.DataSource.DataSourceTables[i].Columns = eval($scope.exercise.DataSource.DataSourceTables[i].Columns);
    }
});