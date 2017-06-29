import {inject} from 'aurelia-framework';
import {EagerExerciseDataLoader}  from 'services/EagerExerciseDataLoader';

@inject(EagerExerciseDataLoader)
export class QueryExercise
{
    constructor(eagerExerciseDataLoader)
    {
        this.dataLoader = eagerExerciseDataLoader;
    }

    activate(params)
    {
        this.dataLoader.getExercise(params.id).then((data) => {
            this.exercise = data; 
            this.showNextQuestion()
        });
    }

    checkAnswer(option)
    {
        // Needs to go off to the server to do this
        // Needs implementing
    }

    showModal(message)
    {
        this.modalMessage = message;
        this.modalActive = true;
    }

    continueClicked() 
    {
        this.modalActive = false;
        if(this.clickedCorrectAnswer) this.showNextQuestion();
    }

    showNextQuestion() 
    {
        let currentQuestionOrder = -1;
        if(this.currentQuestion !== undefined) currentQuestionOrder= this.currentQuestion.order;

        if(currentQuestionOrder + 1 == this.exercise.exerciseQuestions.length) 
        {
            this.finishedExercise();
            return;
        }

        this.runNextQuestionAnimation();
        this.currentQuestion = this.exercise.exerciseQuestions[currentQuestionOrder + 1];
    }

    runNextQuestionAnimation() 
    {
        this.runAnimation = true;
        setTimeout(() => { 
            this.runAnimation = false;
        }, 300);
    }

    finishedExercise()
    {
        this.showModal("Congratulations! You answered all the questions successfully.");
    }
}