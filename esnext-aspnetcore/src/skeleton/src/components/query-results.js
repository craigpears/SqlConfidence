import {bindable} from 'aurelia-framework';
import {HttpClient, json} from 'aurelia-fetch-client';
import 'fetch';

export class QueryResults{
    @bindable sqlquery = '';

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
                      'X-Requested-With': 'Fetch',
                      'Content-Type': 'application/json'
                  }
              })
        });

    }

    sqlqueryChanged(value) {
        this.httpClient.fetch('userquery', {
            method: 'post',
            body: JSON.stringify({sqlquery: this.sqlquery})
        })
        .then(response => response.json())
        .then(data => this.resultsData = data);
    }
}