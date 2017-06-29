using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace DataAccess.Models.Mapping
{
    public class DataSourceMap : EntityTypeConfiguration<DataSource>
    {
        public DataSourceMap()
        {
            // Primary Key
            this.HasKey(t => t.DataSourceId);

            // Properties
            this.Property(t => t.Name)
                .IsRequired()
                .HasMaxLength(100);

            this.Property(t => t.CreatedBy)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.UpdatedBy)
                .HasMaxLength(50);

            this.Property(t => t.DeletedBy)
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("DATA_SOURCES");
            this.Property(t => t.DataSourceId).HasColumnName("DS_ID");
            this.Property(t => t.Name).HasColumnName("DS_NAME");
            this.Property(t => t.CreatedDate).HasColumnName("DS_CREATED_DATE");
            this.Property(t => t.CreatedBy).HasColumnName("DS_CREATED_BY");
            this.Property(t => t.UpdatedDate).HasColumnName("DS_UPDATED_DATE");
            this.Property(t => t.UpdatedBy).HasColumnName("DS_UPDATED_BY");
            this.Property(t => t.Deleted).HasColumnName("DS_DELETED");
            this.Property(t => t.DeletedDate).HasColumnName("DS_DELETED_DATE");
            this.Property(t => t.DeletedBy).HasColumnName("DS_DELETED_BY");
            this.Property(t => t.DataSourceIdGuid).HasColumnName("DS_ID_GUID");
        }
    }
}
