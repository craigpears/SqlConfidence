import {bindable} from 'aurelia-framework';  

export class SqlCodeEditor {
    attached() {
        var textArea = document.getElementById("codemirrorTextArea");
        console.log("activating");
        var editor = CodeMirror.fromTextArea(textArea, {
            lineNumbers: true,
            matchBrackets: true,
            indentUnit: 4,
            mode: "text/x-mssql",
            lineWrapping: true
        });
    }
}