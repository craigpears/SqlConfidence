import {bindable} from 'aurelia-framework';

export class Card{
    @bindable title = '';
    @bindable description = '';

    constructor()
    {
    }
}