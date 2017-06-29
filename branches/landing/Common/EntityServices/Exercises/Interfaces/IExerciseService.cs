using Common.Models;
using DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Exercises.Interfaces
{
    public interface IExerciseService
    {
        Exercise Get(int Id, UserModel user);
        List<Exercise> GetAll(UserModel user);
        List<Exercise> GetAllByUpdatedDate();
        void Save(Exercise Exercise, UserModel user);
    }
}
