using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Models.Mapping
{
    public class ExerciseQuestionUnitTestMap : EntityTypeConfiguration<ExerciseQuestionUnitTest>
    {
        public ExerciseQuestionUnitTestMap()
        {
            // Primary Key
            this.HasKey(t => t.ExerciseQuestionUnitTestId);

            // Properties
            this.Property(t => t.Name)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.Description)
                .IsRequired()
                .HasMaxLength(500);

            this.Property(t => t.SqlToRun)
                .IsRequired();

            this.Property(t => t.SqlToCompare)
                .IsRequired();

            this.Property(t => t.CreatedBy)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.CreatedDate)
                .IsRequired();

            this.Property(t => t.UpdatedBy)
                .HasMaxLength(50);

            this.Property(t => t.DeletedBy)
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("EXERCISES_QUESTIONS_UNIT_TESTS");
            this.Property(t => t.ExerciseQuestionUnitTestId).HasColumnName("EXQUT_ID");
            this.Property(t => t.ExerciseQuestionId).HasColumnName("EXQ_ID");
            this.Property(t => t.Name).HasColumnName("EXQUT_NAME");
            this.Property(t => t.Description).HasColumnName("EXQUT_DESCRIPTION");
            this.Property(t => t.CreatedBy).HasColumnName("EXQUT_CREATED_BY");
            this.Property(t => t.CreatedDate).HasColumnName("EXQUT_CREATED_DATE");
            this.Property(t => t.UpdatedBy).HasColumnName("EXQUT_UPDATED_BY");
            this.Property(t => t.UpdatedDate).HasColumnName("EXQUT_UPDATED_DATE");
            this.Property(t => t.Deleted).HasColumnName("EXQUT_DELETED");
            this.Property(t => t.DeletedBy).HasColumnName("EXQUT_DELETED_BY");
            this.Property(t => t.DeletedDate).HasColumnName("EXQUT_DELETED_DATE");
            this.Property(t => t.SqlToRun).HasColumnName("EXQUT_SQL_TO_RUN");
            this.Property(t => t.SqlToCompare).HasColumnName("EXQUT_SQL_TO_COMPARE");

            // Relationships
            this.HasRequired(t => t.ExerciseQuestion)
                .WithMany(t => t.ExerciseQuestionUnitTests)
                .HasForeignKey(t => t.ExerciseQuestionId);
        }
    }
}
