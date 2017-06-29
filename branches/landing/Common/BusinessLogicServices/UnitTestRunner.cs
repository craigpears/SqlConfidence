using Common.Models;
using DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    public class UnitTestRunner
    {
        public Boolean RunUnitTest(ExerciseQuestionUnitTest test, UserModel user)
        {
            var database = new Database(new DataAccess());
                        
            DataDifferencesModel differences;
            database.CheckAnswer(test.ExerciseQuestionId, test.SqlToCompare, user.UserId, out differences, test.SqlToCompare, test.SqlToRun);
            if(differences.Differences.Any())
            {
                return false;
            }
            else
            {
                return true;
            }

            throw new ArgumentException("Invalid unit test type");
        }
    }
}
