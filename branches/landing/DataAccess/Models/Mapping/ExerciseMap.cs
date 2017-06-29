using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace DataAccess.Models.Mapping
{
    public class ExerciseMap : EntityTypeConfiguration<Exercise>
    {
        public ExerciseMap()
        {
            // Primary Key
            this.HasKey(t => t.ExerciseId);

            // Properties
            this.Property(t => t.Name)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.Summary)
                .IsRequired()
                .HasMaxLength(255);

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

            this.Property(t => t.PublishedBy)
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("EXERCISES");
            this.Property(t => t.ExerciseId).HasColumnName("EX_ID");
            this.Property(t => t.DataSourceId).HasColumnName("DS_ID");
            this.Property(t => t.Name).HasColumnName("EX_NAME");
            this.Property(t => t.Summary).HasColumnName("EX_SUMMARY");
            this.Property(t => t.Description).HasColumnName("EX_DESCRIPTION");
            this.Property(t => t.CreatedDate).HasColumnName("EX_CREATED_DATE");
            this.Property(t => t.CreatedBy).HasColumnName("EX_CREATED_BY");
            this.Property(t => t.UpdatedDate).HasColumnName("EX_UPDATED_DATE");
            this.Property(t => t.UpdatedBy).HasColumnName("EX_UPDATED_BY");
            this.Property(t => t.Deleted).HasColumnName("EX_DELETED");
            this.Property(t => t.DeletedDate).HasColumnName("EX_DELETED_DATE");
            this.Property(t => t.DeletedBy).HasColumnName("EX_DELETED_BY");
            this.Property(t => t.Published).HasColumnName("EX_PUBLISHED");
            this.Property(t => t.PublishedDate).HasColumnName("EX_PUBLISHED_DATE");
            this.Property(t => t.PublishedBy).HasColumnName("EX_PUBLISHED_BY");
            this.Property(t => t.SectionName).HasColumnName("EX_SECTION_NAME");
            this.Property(t => t.Order).HasColumnName("EX_ORDER");
            this.Property(t => t.ShowQueryBuilder).HasColumnName("EX_SHOW_QUERY_BUILDER");
            this.Property(t => t.ExerciseIdGuid).HasColumnName("EX_ID_GUID");

            // Relationships
            this.HasRequired(t => t.DataSource)
                .WithMany(t => t.Exercises)
                .HasForeignKey(d => d.DataSourceId);

        }
    }
}
