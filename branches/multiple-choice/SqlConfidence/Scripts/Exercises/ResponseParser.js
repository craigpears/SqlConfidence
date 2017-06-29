sqlConfidenceApp.service('responseParserService', function () {
    this.parseResponse = function (response) {
        var returnObject = {};
        returnObject.headers = [];
        returnObject.rows = [];
        angular.forEach(response.data.Columns, function (column) {
            returnObject.headers.push(column.Name);
        });
        returnObject.rows = response.data.Rows;

        // Handle any serialised date objects
        angular.forEach(returnObject.rows, function (row) {
            for (var prop in row) {
                var value = row[prop];
                if (value !== null && value.substring !== undefined && value.substring(0, 5) === '/Date') {
                    // If the value is a serialised date object, convert it into a better string representation
                    var dateObject = new Date(parseInt(value.substr(6)));
                    row[prop] = dateObject.getFullYear() + '-' + toStringAndPad(dateObject.getMonth() + 1) + '-' + toStringAndPad(dateObject.getDay()) + ' ' + toStringAndPad(dateObject.getHours()) + ':' + toStringAndPad(dateObject.getMinutes()) + ':' + toStringAndPad(dateObject.getSeconds());
                }
            }
        });

        return returnObject;
    }

    function toStringAndPad(num) {
        var str = num.toString();
        return str.length === 1 ? "0" + str : str;
    }
});