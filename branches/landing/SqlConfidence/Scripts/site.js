var currentQuestion;
var correctAnswers;
var codeMirror;
var questions;
//var questions declared in Exercise.cshtml
var showMessageInterval;

function OpenExercise(exercise_id) {
    location.href = "/Exercise/Exercise/?ExerciseId=" + exercise_id;
}

function SubmitForm(formId, validationFunction) {
    var result = validationFunction(formId);
    if (!result) {
        return false;
    }
    $("#" + formId).submit();
}

function ValidateRegistrationForm(formId) {
    // Check that they have agreed to the conditions
    var agreedConditions = $("#AgreeConditions").is(':checked');
    if (!agreedConditions) {
        ShowErrorMessage("You must agree to the terms and conditions");
        return false;
    }

    // Check that the password fields match
    var password = $("input[name=PlainPassword]").val();
    var passwordConfirm = $("input[name=PlainPasswordConfirm]").val();
    if (password != passwordConfirm) {
        ShowErrorMessage("Passwords do not match");
        return false;
    }

    var result = ValidateRequiredFields();
    if (!result) return false;

    return true;
}

function ValidateLoginForm(formId) {
    var result = ValidateRequiredFields();
    if (!result) return false;

    return true;
}

function ValidateRequiredFields() {
    var errorRaised = false;
    $("input[data-required='true']").each(function (index, element) {
        if ($(element).val() == '') {
            ShowErrorMessage("Please fill in all required fields");
            errorRaised = true;
        }
    });

    return !errorRaised;
}

function ShowSuccessMessage(message, timeToHide)
{
    if (timeToHide == undefined) timeToHide = 10000;
    $("#success_bar").addClass("active");
    $("#success_bar #success_message").html(message);
    if(timeToHide >= 0) showMessageInterval = setInterval(HideSuccessMessage, timeToHide);
}

function HideSuccessMessage() {
    clearInterval(showMessageInterval);
    $("#success_bar").removeClass("active");
}

function ShowErrorMessage(message, timeToHide) {
    if (timeToHide == undefined) timeToHide = 10000;
    $("#error_bar").addClass("active");
    $("#error_bar #error_message").html(message);
    if (timeToHide >= 0) showMessageInterval = setInterval(HideErrorMessage, timeToHide);
}

function HideErrorMessage() {
    clearInterval(showMessageInterval);
    $("#error_bar").removeClass("active");
}

function PublishExercise(ex_id) {
    if (confirm("Are you sure you want to publish this exercise?")) {
        $.post("/Admin/PublishExercise?ExerciseId=" + ex_id, function (data) {
            if (!data.Success) {
                ShowErrorMessage(data.ErrorMessage);
            }
            else {
                ShowSuccessMessage("Exercise Published Successfully");
                $("#Published").checked(true);
            }
        });
    }    
}

function MoveQuestionUp(question_id) {
    $element = $("#question_" + question_id);
    var order = $element.data("order");
    $elementTwo = $("*[data-order='" + (order - 1) + "']");
    var question_two_id = $elementTwo.data("exercise-id");
    $.post("/Admin/MoveQuestions?QuestionOne=" + question_id + "&QuestionTwo=" + question_two_id, function (data) {
        if (!data.Success) {
            ShowErrorMessage(data.ErrorMessage);
        }
        else {
            RefreshQuestionsSublist();
        }
    });
}

function MoveQuestionDown(question_id) {
    $element = $("#question_" + question_id);
    var order = $element.data("order");
    $elementTwo = $("*[data-order='" + (order + 1) + "']");
    var question_two_id = $elementTwo.data("exercise-id");
    $.post("/Admin/MoveQuestions?QuestionOne=" + question_two_id + "&QuestionTwo=" + question_id, function (data) {
        if (!data.Success) {
            ShowErrorMessage(data.ErrorMessage);
        }
        else {
            RefreshQuestionsSublist();
        }
    });
}

function RefreshQuestionsSublist() {
    $.post("/Admin/QuestionsSublist?ExerciseId=" + $("#ExerciseId").val(), function (data) {
        $("#questions-sublist").html(data);
    });
}

function AddDataSourceTable(tableName, dataSourceId)
{
    $.post("/Admin/AddDataSourceTable?TableName=" + tableName + "&DataSourceId=" + dataSourceId, function (data) {
        if (!data.Success) {
            ShowErrorMessage(data.ErrorMessage);
        }
        else
        {
            ShowSuccessMessage("Added successfully");
            $("#add_table_" + tableName).hide();
        }
    });
}

function PutExerciseToServer(exerciseId)
{
    $.post("/Admin/PutExerciseToServer?ExerciseId=" + exerciseId, function (data) {
        if (!data.Success) {
            ShowErrorMessage(data.ErrorMessage);
        }
        else {
            ShowSuccessMessage("Put successfully");
        }
    });
}