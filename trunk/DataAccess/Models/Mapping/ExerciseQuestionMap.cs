using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace DataAccess.Models.Mapping
{
    public class ExerciseQuestionMap : EntityTypeConfiguration<ExerciseQuestion>
    {
        public ExerciseQuestionMap()
        {
            // Primary Key
            this.HasKey(t => t.ExerciseQuestionId);

            // Properties
            this.Property(t => t.AnswerTemplate)
                .IsRequired()
                .HasMaxLength(2000);

            this.Property(t => t.Description)
                .IsRequired()
                .HasMaxLength(500);

            this.Property(t => t.CreatedBy)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.UpdatedBy)
                .HasMaxLength(50);

            this.Property(t => t.DeletedBy)
                .HasMaxLength(50);

            this.Property(t => t.StartingSql)
                .HasMaxLength(2000);

            // Table & Column Mappings
            this.ToTable("EXERCISES_QUESTIONS");
            this.Property(t => t.ExerciseQuestionId).HasColumnName("EXQ_ID");
            this.Property(t => t.ExerciseId).HasColumnName("EX_ID");
            this.Property(t => t.InstructionsTemplate).HasColumnName("EXQ_INSTRUCTIONS_TEMPLATE");
            this.Property(t => t.AnswerTemplate).HasColumnName("EXQ_ANSWERS_TEMPLATE");
            this.Property(t => t.Description).HasColumnName("EXQ_DESCRIPTION");
            this.Property(t => t.CreatedBy).HasColumnName("EXQ_CREATED_BY");
            this.Property(t => t.CreatedDate).HasColumnName("EXQ_CREATED_DATE");
            this.Property(t => t.UpdatedBy).HasColumnName("EXQ_UPDATED_BY");
            this.Property(t => t.UpdatedDate).HasColumnName("EXQ_UPDATED_DATE");
            this.Property(t => t.Deleted).HasColumnName("EXQ_DELETED");
            this.Property(t => t.DeletedBy).HasColumnName("EXQ_DELETED_BY");
            this.Property(t => t.DeletedDate).HasColumnName("EXQ_DELETED_DATE");
            this.Property(t => t.Hint).HasColumnName("EXQ_HINT");
            this.Property(t => t.Order).HasColumnName("EXQ_ORDER");
            this.Property(t => t.StartingSql).HasColumnName("EXQ_STARTING_SQL");
            this.Property(t => t.ExerciseQuestionIdGuid).HasColumnName("EXQ_ID_GUID");
            this.Property(t => t.ExerciseQuestionType).HasColumnName("EXQ_TYPE");

            // Relationships
            this.HasRequired(t => t.Exercise)
                .WithMany(t => t.ExerciseQuestions)
                .HasForeignKey(d => d.ExerciseId);

        }
    }
}
