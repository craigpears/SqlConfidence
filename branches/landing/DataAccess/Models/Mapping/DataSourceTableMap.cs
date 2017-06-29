using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace DataAccess.Models.Mapping
{
    public class DataSourceTableMap : EntityTypeConfiguration<DataSourceTable>
    {
        public DataSourceTableMap()
        {
            // Primary Key
            this.HasKey(t => t.DataSourceTableId);

            // Properties
            this.Property(t => t.TableName)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.TableAlias)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.CreatedBy)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.UpdatedBy)
                .HasMaxLength(50);

            this.Property(t => t.DeletedBy)
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("DATA_SOURCES_TABLES");
            this.Property(t => t.DataSourceTableId).HasColumnName("DST_ID");
            this.Property(t => t.DataSourceId).HasColumnName("DS_ID");
            this.Property(t => t.TableName).HasColumnName("DST_TABLE_NAME");
            this.Property(t => t.TableAlias).HasColumnName("DST_TABLE_ALIAS");
            this.Property(t => t.CreatedDate).HasColumnName("DST_CREATED_DATE");
            this.Property(t => t.CreatedBy).HasColumnName("DST_CREATED_BY");
            this.Property(t => t.UpdatedDate).HasColumnName("DST_UPDATED_DATE");
            this.Property(t => t.UpdatedBy).HasColumnName("DST_UPDATED_BY");
            this.Property(t => t.Deleted).HasColumnName("DST_DELETED");
            this.Property(t => t.DeletedDate).HasColumnName("DST_DELETED_DATE");
            this.Property(t => t.DeletedBy).HasColumnName("DST_DELETED_BY");
            this.Property(t => t.Columns).HasColumnName("DST_COLUMNS");
            this.Property(t => t.DataSourceTableIdGuid).HasColumnName("DST_ID_GUID");

            // Relationships
            this.HasRequired(t => t.DataSource)
                .WithMany(t => t.DataSourceTables)
                .HasForeignKey(d => d.DataSourceId);

        }
    }
}
