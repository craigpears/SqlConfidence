sqlConfidenceApp.filter('answeredQuestions', function () {
    return function (questions) {
        var returnQuestions = new Array();
        for (var i = 0; i < questions.length; i++) {
            if (questions[i].Answered) returnQuestions.push(questions[i]);
        }
        return returnQuestions;
    };
});

sqlConfidenceApp.filter('unansweredQuestions', function () {
    return function (questions) {
        var returnQuestions = new Array();
        for (var i = 0; i < questions.length; i++) {
            if (!questions[i].Answered) returnQuestions.push(questions[i]);
        }
        return returnQuestions;
    };
});