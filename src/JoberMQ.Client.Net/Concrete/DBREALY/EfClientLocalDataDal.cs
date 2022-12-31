using GenRep.EntityFramework;
using JoberMQ.Client.Common.Dbos;
using JoberMQ.Client.Net.Abstract.DBREALY;
using Microsoft.EntityFrameworkCore;

namespace JoberMQ.Client.Net.Concrete.DBREALY.EF
{
    internal class EfClientLocalDataDal : EfRepository<ClientLocalDataDbo>, IClientLocalDataDal
    {
        public EfClientLocalDataDal(Func<DbContext> dbContext) : base(dbContext)
        {
        }
    }
}