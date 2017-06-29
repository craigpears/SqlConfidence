using Common.Enums;
using Common.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace Common
{
    public class DifferenceEngine
    {
        public Boolean StopAtFirstDifference { get; set; }
        public Boolean LookForSimilarRows { get; set; }

        public DifferenceEngine(Boolean StopAtFirstDifference =false, Boolean LookForSimilarRows = false)
        {
            this.StopAtFirstDifference = StopAtFirstDifference;
            this.LookForSimilarRows = LookForSimilarRows;
        }

        public DataDifferencesModel GetDifferences(DataTable userData, DataTable answerData)
        {
            DataDifferencesModel differences = new DataDifferencesModel();
            differences.UserDataRowCount = userData.Rows.Count;
            differences.AnswerDataRowCount = answerData.Rows.Count;
            differences.UserDataColumnCount = userData.Columns.Count;
            differences.AnswerDataColumnCount = answerData.Columns.Count;

            // Check that they have the same number of rows
            if (userData.Rows.Count != answerData.Rows.Count)
            {
                DataDifferenceModel difference = new DataDifferenceModel();
                difference.DifferenceType = DataDifferenceType.RowCountDifferent;
                differences.Differences.Add(difference);
            }

            // Check that they have the same number of columns
            // If the number of columns don't match, don't do any further comparisons
            if (userData.Columns.Count != answerData.Columns.Count)
            {
                DataDifferenceModel difference = new DataDifferenceModel();
                difference.DifferenceType = DataDifferenceType.ColumnCountDifferent;
                differences.Differences.Add(difference);
                return differences;
            }

            // Check that each of the columns is the same
            Boolean foundDifferentColumnNames = false;
            for (int i = 0; i < userData.Columns.Count; i++)
            {
                if (userData.Columns[i].ColumnName != answerData.Columns[i].ColumnName)
                {
                    DataDifferenceModel difference = new DataDifferenceModel();
                    difference.DifferenceType = DataDifferenceType.ColumnMismatch;
                    difference.ColumnDifferencePosition = i;
                    difference.AnswerQueryColumn = answerData.Columns[i].ColumnName;
                    difference.UserQueryColumn = userData.Columns[i].ColumnName;
                    differences.Differences.Add(difference);
                    foundDifferentColumnNames = true;
                }
            }

            if (foundDifferentColumnNames) return differences;

            var answerDataLookup = new Dictionary<string, DataRow>();
            foreach (DataRow row in answerData.Rows)
            {
                var hash = GetHash(row);
                if(!answerDataLookup.ContainsKey(hash))
                {
                    answerDataLookup.Add(hash, row);
                }
            }

            // If the columns match, we can do a more detailed row comparison
            for (int i = 0; i < userData.Rows.Count; i++)
            {
                Boolean foundInSamePosition = false;
                Boolean foundAtAll = false;
                Boolean similarMatchFound = false;
                int answerQueryPosition = 0;

                // First check if there was a match in the same position or not
                if (answerData.Rows.Count > i && DataRowsSame(userData.Rows[i], answerData.Rows[i]))
                {
                    foundInSamePosition = true;
                    foundAtAll = true;
                }

                // If it wasn't found there, check if it was in the other result set at all
                if (!foundInSamePosition && !StopAtFirstDifference)
                {
                    var dataHash = GetHash(userData.Rows[i]);
                    var identicalRowFound = answerDataLookup.ContainsKey(dataHash);
                    if(identicalRowFound)
                    {
                        var identicalRow = answerDataLookup[dataHash];
                        foundAtAll = true;
                    }

                    if(LookForSimilarRows)
                    {
                        for (int j = 0; j < answerData.Rows.Count; j++)
                        {
                            if (DataRowsSimilar(userData.Rows[i], answerData.Rows[j]))
                            {
                                similarMatchFound = true;
                                answerQueryPosition = j;
                            }
                        }
                    }
                    
                }

                // Log a difference if it wasn't found in the same position
                if (!foundInSamePosition)
                {
                    DataDifferenceModel difference = new DataDifferenceModel();
                    difference.UserQueryRow = userData.Rows[i];
                    difference.UserQueryPosition = i;

                    if (foundAtAll)
                    {
                        difference.DifferenceType = DataDifferenceType.WrongOrder;
                        difference.AnswerQueryPosition = answerQueryPosition;
                    }
                    else if(similarMatchFound)
                    {
                        difference.DifferenceType = DataDifferenceType.SimilarMatchFound;
                        difference.AnswerQueryPosition = answerQueryPosition;
                        difference.AnswerQueryRow = answerData.Rows[answerQueryPosition];
                    }
                    else
                    {
                        difference.DifferenceType = DataDifferenceType.NotFound;
                    }

                    differences.Differences.Add(difference);
                    if(StopAtFirstDifference)
                    {
                        return differences;
                    }
                }
            }

            answerDataLookup = null;
            return differences;
        }

        public String GetHash(DataRow row)
        {
            string hash = "";
            foreach (var item in row.ItemArray)
            {
                hash += item.ToString();
            }
            return hash;
        }

        public Boolean DataRowsSame(DataRow row1, DataRow row2)
        {
            var array1 = row1.ItemArray;
            var array2 = row2.ItemArray;
            Boolean equal = array1.SequenceEqual(array2);
            return equal;
        }

        public Boolean DataRowsSimilar(DataRow row1, DataRow row2)
        {
            // A data row is similar if most of the values it contains are the same
            // For example, a count for an aggregate could be slightly wrong
            // Or a boolean in a case statement could be false instead of true
            var numberOfColumns = row1.ItemArray.Length;
            Double equalValues = 0;
            Double differentValues = 0;
            Double similarValues = 0;
            for (int i = 0; i < row1.ItemArray.Length; i++)
            {
                // If the column name has id in it, then it definitely shouldn't be similar
                if(row1.Table.Columns[i].ColumnName.ToLower().Contains("_id"))
                {
                    return false;
                }

                if(row1.ItemArray[i].Equals(row2.ItemArray[i]))
                {
                    equalValues++;
                }
                else
                {
                    if (AreSimilarNumbers(row1.ItemArray[i], row2.ItemArray[i]))
                    {
                        similarValues++;
                    }
                    else
                    {
                        differentValues++;
                    }
                }
            }

            Double equalToDifferentRatio = differentValues / equalValues;
            return equalToDifferentRatio <= 0.3;
        }

        public bool AreSimilarNumbers(Object obj1, Object obj2)
        {
            try
            {
                Double firstNumber = Double.Parse(obj1.ToString());
                Double secondNumber = Double.Parse(obj2.ToString());

                if (firstNumber == secondNumber) return true;

                Double smallest = Math.Min(firstNumber, secondNumber);
                Double largest = Math.Max(firstNumber, secondNumber);

                if(largest - smallest <= 2)
                {
                    return true;
                }

                Double ratio = largest / smallest;

                return ratio <= 1.2;
            }
            catch{}

            return false;
        }
    }
}
