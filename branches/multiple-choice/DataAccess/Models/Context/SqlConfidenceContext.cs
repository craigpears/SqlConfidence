using System.Data.Entity;
using System.Data.Entity.Infrastructure;

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
            this.Configuration.ProxyCreationEnabled = false;
        }

        public DbSet<DataSource> DataSources { get; set; }
        public DbSet<DataSourceTable> DataSourceTables { get; set; }
        public DbSet<Exercise> Exercises { get; set; }
        public DbSet<MultipleChoiceExercise> MultipleChoiceExercises { get; set; }
        public DbSet<MultipleChoiceQuestion> MultipleChoiceQuestions { get; set; }
        public DbSet<ExerciseQuestion> ExerciseQuestions { get; set; }
        public DbSet<ExerciseQuestionChoice> ExerciseQuestionChoices { get; set; }
        public DbSet<ExerciseQuestionUnitTest> ExerciseQuestionUnitTests { get; set; }
        public DbSet<ExerciseQuestionAnswered> ExerciseQuestionAnswereds { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<UserAction> UserActions { get; set; }
        public DbSet<UserActionType> UserActionTypes { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            // Entity framework was getting confused with the inheritance + relationship on the inherited type and generating multiple foreign keys
            // so specify the relationship manually
            modelBuilder.Entity<MultipleChoiceOption>().HasRequired(x => x.ExerciseQuestion).WithMany(x => x.Options).HasForeignKey(x => x.ExerciseQuestionId);
        }
    }
}
