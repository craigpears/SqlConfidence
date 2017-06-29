sqlConfidenceApp.directive("databaseSchema", function () {
    return {
        restrict: 'E',
        scope: {
            tables: '='
        },
        controller: function($scope) {
            $scope.ToggleTableTreeContents = function (table) {
                $scope.toggleShowData[table.DataSourceTableId].showTable = !$scope.toggleShowData[table.DataSourceTableId].showTable;
            }

            $scope.ToggleFolder = function (table, folder) {
                if (folder == 'columns') {
                    $scope.toggleShowData[table.DataSourceTableId].showColumns = !$scope.toggleShowData[table.DataSourceTableId].showColumns;
                }
            }

            $scope.GetPlusMinus = function (table) {
                if($scope.GetShowData(table.DataSourceTableId).showTable)
                {
                    return "-";
                }
                else
                {
                    return "+";
                }
            }

            $scope.toggleShowData = {};

            // Add some extra display logic fields into the view model
            for (var i = 0; i < $scope.tables.length; i++) {
                var table = $scope.tables[i];
                $scope.toggleShowData[table.DataSourceTableId] = { showTable: false, showColumns: true };
            }

            $scope.GetShowData = function(tableId) {
                return $scope.toggleShowData[tableId];
            }
        },
        templateUrl: '/Exercise/Exercise_Partial_DatabaseSchema'
    }
});