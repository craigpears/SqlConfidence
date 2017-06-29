using Microsoft.EntityFrameworkCore;

namespace DataAccess.Models
{
    public partial class SqlConfidenceContext : DbContext
    {
        public SqlConfidenceContext(DbContextOptions<SqlConfidenceContext> options)
            : base(options)
        {
        }

        public DbSet<DataSource> DataSources { get; set; }
        public DbSet<DataSourceTable> DataSourceTables { get; set; }

        public DbSet<Section> Sections { get; set; }
        public DbSet<MultipleChoiceExercise> MultipleChoiceExercises { get; set; }
        public DbSet<MultipleChoiceQuestion> MultipleChoiceQuestions { get; set; }

        public DbSet<QueryBuilderExercise> QueryBuilderExercises { get; set; }
        public DbSet<QueryBuilderQuestion> QueryBuilderQuestions { get; set; }

        public DbSet<QueryExercise> QueryExercises { get; set; }
        public DbSet<QueryQuestion> QueryQuestions { get; set; }

        public DbSet<UnitTestedExercise> UnitTestedExercises { get; set; }
        public DbSet<UnitTestedQuestion> UnitTestedQuestions { get; set; }

        public DbSet<ExerciseQuestionAnswered> ExerciseQuestionAnswereds { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<UserAction> UserActions { get; set; }
        public DbSet<UserActionType> UserActionTypes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Entity framework was getting confused with the inheritance + relationship on the inherited type and generating multiple foreign keys
            // so specify the relationship manually
            modelBuilder.Entity<MultipleChoiceOption>().HasOne(x => x.ExerciseQuestion).WithMany(x => x.Options).HasForeignKey(x => x.ExerciseQuestionId);
        }
    }
}
