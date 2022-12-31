using JoberMQ.Client.Common.Dbos;
using JoberMQ.Client.Net.Mappings.Base;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
