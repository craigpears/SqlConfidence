import {bindable} from 'aurelia-framework';

export class ScModal {
    @bindable message = '';
    @bindable active = false;
    @bindable oncontinue = function(){console.log("no bind")};
}