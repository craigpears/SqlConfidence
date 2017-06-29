import {inject} from 'aurelia-framework';
import {EagerExerciseDataLoader}  from 'services/EagerExerciseDataLoader';

@inject(EagerExerciseDataLoader)
export class ListExercises {
    exercises = [];

    constructor(eagerExerciseDataLoader)
    {
        this.dataLoader = eagerExerciseDataLoader;
        this.dataLoader.getSections().then((data) => this.sections = data);
        this.dataLoader.getExercises().then((data) => this.exercises = data);
    }

    getlink(exercise){
        switch(exercise.discriminator)
        {
            case "MultipleChoiceExercise":
                return "/multiple-choice-exercise/" + exercise.id;
            case "QueryExercise":
                return "/query-exercise/" + exercise.id;
            case "QueryBuilderExercise":
                return "/query-builder-exercise/" + exercise.id;
            case "UnitTestedExercise":
                return "/unit-tested-exercise/" + exercise.id;
            default:
                return "";
        }
    }
}
