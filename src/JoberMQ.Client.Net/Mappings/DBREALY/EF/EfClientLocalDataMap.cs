using JoberMQ.Client.Net.Mappings.Base;
using JoberMQ.Common.Dbos;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace JoberMQ.Client.Net.Mappings.DBREALY.EF
{
    internal class EfClientLocalDataMap : BasePropertyGuidTypeConfiguration<ClientLocalDataDbo>
    {
        public override void Configure(EntityTypeBuilder<ClientLocalDataDbo> builder)
        {
            this.tableName = "LocalData";

            builder
                .Property(t => t.PushDataType)
                .IsRequired(true);

            builder
                .Property(t => t.JobId)
                .IsRequired(false);

            builder
              .Property(t => t.Data)
              .IsRequired(false);

            base.Configure(builder);
        }
    }
}
