import 'fetch';
import {HttpClient, json} from 'aurelia-fetch-client';

export class EagerExerciseDataLoader{

    constructor()
    {
        this.httpClient = new HttpClient();
        this.httpClient.configure(config => {
            config
              .useStandardConfiguration()
              .withBaseUrl('api/')
              .withDefaults({
                  credentials: 'same-origin',
                  headers: {
                      'X-Requested-With': 'Fetch'
                  }
              })
        });


        this.httpClient.fetch('exercise')
            .then(response => response.json())
            .then(data => this.exercisesResponse = data);

        this.httpClient.fetch('section')
            .then(response => response.json())
            .then(data => this.sectionsResponse = data);
    }

    getExercises()
    {
        return new Promise((resolve, reject) => {
            this.tryGetExercises(resolve);
        });
    }

    tryGetExercises(resolve)
    {
        if(this.exercisesResponse == null)
            setTimeout(() => {this.tryGetExercises(resolve);}, 20);
        else
            resolve(this.exercisesResponse);
    }

    getExercise(id)
    {
        return new Promise((resolve, reject) => {
            var tryFunction = () => {
                if(this.exercisesResponse == null) setTimeout(() => {tryFunction();}, 20);
                else resolve(this.getExerciseImplementation(id));
            };
            tryFunction();
        });
    }

    getExerciseImplementation(id)
    {
        for(let i = 0; i < this.exercisesResponse.length; i++)
        {
            let exercise = this.exercisesResponse[i];
            if(exercise.id == id) {
                return exercise;
            }
        }

        return null;
    }

    getSections()
    {
        return new Promise((resolve, reject) => {
            var tryFunction = () => {
                if(this.sectionsResponse == null) setTimeout(() => tryFunction(resolve));
                else resolve(this.sectionsResponse);
            };
            tryFunction();
        });
    }
}