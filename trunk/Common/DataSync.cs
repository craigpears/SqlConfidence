using Common.Models;
using DataAccess.Models;
using DataAccess.Server;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    public class DataSync
    {
        public void PutExerciseToServer(int ExerciseId)
        { 
            var serverDA = new ServerDataAccess();
            var context = new SqlConfidenceContext();
            var exercise = context.Exercises.Find(ExerciseId);
            serverDA.PutExercise(exercise);
        }
    }
}
