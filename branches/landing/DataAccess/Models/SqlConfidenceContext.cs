using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using DataAccess.Models.Mapping;

namespace DataAccess.Models
{
    public partial class SqlConfidenceContext : DbContext
    {
        static SqlConfidenceContext()
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<SqlConfidenceContext, Migrations.Configuration>("SqlConfidenceContext"));
        }

        public SqlConfidenceContext()
            : base("Name=SqlConfidenceContext")
        {
            this.Configuration.LazyLoadingEnabled = false;
        }

        public DbSet<DataSource> DataSources { get; set; }
        public DbSet<DataSourceTable> DataSourceTables { get; set; }
        public DbSet<Exercise> Exercises { get; set; }
        public DbSet<ExerciseQuestion> ExerciseQuestions { get; set; }
        public DbSet<ExerciseQuestionChoice> ExerciseQuestionChoices { get; set; }
        public DbSet<ExerciseQuestionUnitTest> ExerciseQuestionUnitTests { get; set; }
        public DbSet<ExerciseQuestionAnswered> ExerciseQuestionAnswereds { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<UserAction> UserActions { get; set; }
        public DbSet<UserActionType> UserActionTypes { get; set; }
        public DbSet<vwExercisesQuestionsUser> vwExercisesQuestionsUsers { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new DataSourceMap());
            modelBuilder.Configurations.Add(new DataSourceTableMap());
            modelBuilder.Configurations.Add(new ExerciseMap());
            modelBuilder.Configurations.Add(new ExerciseQuestionMap());
            modelBuilder.Configurations.Add(new ExerciseQuestionChoiceMap());
            modelBuilder.Configurations.Add(new ExerciseQuestionUnitTestMap());
            modelBuilder.Configurations.Add(new ExerciseQuestionAnsweredMap());
            modelBuilder.Configurations.Add(new UserMap());
            modelBuilder.Configurations.Add(new UserActionMap());
            modelBuilder.Configurations.Add(new UserActionTypeMap());
            modelBuilder.Configurations.Add(new vwExercisesQuestionsUserMap());
        }
    }
}
