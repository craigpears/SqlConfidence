using Common.Models;
using DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Exercises.Interfaces
{
    public interface IExerciseRepository
    {
        Exercise Get(int Id);
        List<Exercise> GetAll();
        List<Exercise> GetAllByUpdatedDate();
        void Save(Exercise Exercise);
    }
}
