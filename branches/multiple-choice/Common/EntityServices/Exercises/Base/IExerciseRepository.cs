namespace Common.EntityServices.Exercises.Base
{
    public interface IExerciseRepository
    {
        int GetExerciseIdFromQuestionId(int id);
        void MarkQuestionAsAnswered(int id, int userId);
    }
}