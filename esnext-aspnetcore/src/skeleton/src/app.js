import {inject} from 'aurelia-framework';
import {EagerExerciseDataLoader}  from './services/EagerExerciseDataLoader';

@inject(EagerExerciseDataLoader)
export class App {
    constructor(eagerExerciseDataLoader) {
        
    }

  configureRouter(config, router) {
      config.title = 'Sql Confidence';
      config.options.pushState = true;
      config.options.root = '/';
    config.map([
      { route: ['', 'home'],                    name: 'home',                     moduleId: 'pages/home',                     nav: true,  title: 'Home' },
      { route: 'exercises',                     name: 'list-exercises',           moduleId: 'pages/list-exercises',           nav: true,  title: 'Exercises' },
      { route: 'multiple-choice-exercise/:id',  name: 'multiple-choice-exercise', moduleId: 'pages/multiple-choice-exercise', nav: false, title: 'Exercise'},
      { route: 'query-exercise/:id',            name: 'query-exercise',           moduleId: 'pages/query-exercise',           nav: false, title: 'Query Exercise'},
      { route: 'living-style-guide',            name: 'living-style-guide',       moduleId: 'pages/living-style-guide',       nav: false, title: 'Living Style Guide'}
    ]);

    this.router = router;
  }
}
