using GenRep.EntityFramework;
using JoberMQ.Client.Net.Abstraction.DBREALY;
using JoberMQ.Common.Dbos;
using Microsoft.EntityFrameworkCore;

namespace JoberMQ.Client.Net.Implementation.DBREALY
{
    internal class EfClientLocalDataDal : EfRepository<ClientLocalDataDbo>, IClientLocalDataDal
    {
        public EfClientLocalDataDal(Func<DbContext> dbContext) : base(dbContext)
        {
        }
    }
}