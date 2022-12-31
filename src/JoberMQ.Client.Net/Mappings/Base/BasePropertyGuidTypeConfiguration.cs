using JoberMQ.Client.Common.Database.Base;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace JoberMQ.Client.Net.Mappings.Base
{
    internal abstract class BasePropertyGuidTypeConfiguration<TBase> : IEntityTypeConfiguration<TBase>
    where TBase : DboPropertyGuidBase
    {
        public string tableName;
        public virtual void Configure(EntityTypeBuilder<TBase> builder)
        {
            #region TABLE AND ID
            builder
                .ToTable(tableName)
                .HasKey(x => x.Id);
            #endregion

            #region GENERAL INFORMTIONS
            builder.Property(t => t.IsActive).HasColumnType("bit").IsRequired(true);
            builder.Property(t => t.IsDelete).HasColumnType("bit").IsRequired(true);
            //builder.Property(t => t.CreateDate).HasColumnType("datetime").HasDefaultValue(DateHelper.GetUniversalNow());
            //builder.Property(t => t.CreateDate).HasColumnType("datetime2(7)").HasDefaultValue(DateHelper.GetUniversalNow());
            builder.Property(t => t.CreateDate).HasColumnType("datetime2(7)");
            //builder.Property(t => t.UpdateDate).HasColumnType("datetime");
            builder.Property(t => t.UpdateDate).HasColumnType("datetime2(7)");
            #endregion

            #region DATA SAVE
            //builder.Property(t => t.ProcessTime).HasColumnType("datetime").HasDefaultValue(DateHelper.GetUniversalNow());
            //builder.Property(t => t.ProcessTime).HasColumnType("datetime2(7)").HasDefaultValue(DateHelper.GetUniversalNow());
            builder.Property(t => t.ProcessTime).HasColumnType("datetime2(7)");
            builder.Property(t => t.DataStatusType);
            #endregion
        }
    }
}
