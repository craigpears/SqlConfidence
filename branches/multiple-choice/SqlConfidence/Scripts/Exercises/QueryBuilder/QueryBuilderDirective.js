sqlConfidenceApp.directive("queryBuilder", function () {
    return {
        restrict: 'E',
        scope: {
            table: '=',
            control: '='
        },
        controller: function($scope) {
            $scope.GetQueryBuilderColumnOptions = function (column) {
                var options = new Array();
                if (column.Type == "varchar" || column.Type == "nvarchar") {
                    options.push({ value: 'equals-string', label: 'Equals' });
                    options.push({ value: 'like', label: 'Like' });
                }
                else if (column.Type == "int" || column.Type == "numeric") {
                    options.push({ value: 'equals-number', label: 'Equals' });
                    options.push({ value: 'greater-than', label: 'Greater Than' });
                    options.push({ value: 'less-than', label: 'Less Than' });
                }
                return options;
            }

            $scope.GetQueryFromBuilder = function () {
                var columns = new Array();
                $.each($scope.table.Columns, function (key, value) {
                    columns.push(value);
                });

                if (columns.length == 0) {
                    ShowErrorMessage("You must select a column");
                    throw "You must select a column";
                }

                // Build the query
                var query = "SELECT ";
                for (var i = 0; i < columns.length; i++) {
                    if (columns[i].Selected) {
                        if (i != 0) query += ", ";
                        query += columns[i].Name;
                    }
                }

                query += " FROM " + $scope.table.TableAlias;

                // Apply any filters
                var includedWhere = false;
                for (var i = 0; i < columns.length; i++) {
                    var filterValue = columns[i].Filter || "";
                    var filterType = columns[i].Where.value;
                    if (filterValue != "") {
                        if (!includedWhere) {
                            query += " WHERE ";
                            includedWhere = true;
                        }
                        else {
                            query += " AND ";
                        }
                        query += columns[i].Name;

                        if (filterType == "equals-string") {
                            query += " = '" + filterValue + "'";
                        }
                        else if (filterType == "like") {
                            query += " LIKE '%" + filterValue + "%'";
                        }
                        else if (filterType == "equals-number") {
                            query += " = " + filterValue;
                        }
                        else if (filterType == "greater-than") {
                            query += " > " + filterValue;
                        }
                        else if (filterType == "less-than") {
                            query += " < " + filterValue;
                        }
                    }
                }

                // Apply any order clause
                var orderColumn = $scope.OrderColumn;
                var orderDirection = $scope.OrderDirection;
                if (orderColumn != "None") {
                    query += " ORDER BY ";
                    query += orderColumn;
                    query += " " + orderDirection;
                }

                return query;
            }

            $scope.ClearQueryBuilder = function () {
                for (var i = 0; i < $scope.table.Columns.length; i++) {
                    var column = $scope.table.Columns[i];
                    column.Selected = true;
                    column.QueryBuilderColumnOptions = $scope.GetQueryBuilderColumnOptions(column);
                    column.Filter = "";
                    column.Where = column.QueryBuilderColumnOptions[0];
                }

                $scope.OrderColumn = 'None';
                $scope.OrderDirection = 'ASC';
            }

            $scope.control.GetQueryFromBuilder = $scope.GetQueryFromBuilder;
            $scope.control.ClearQueryBuilder = $scope.ClearQueryBuilder;
            $scope.ClearQueryBuilder();
        },
        templateUrl: '/Views/Directives/QueryBuilder.html'
    }
});