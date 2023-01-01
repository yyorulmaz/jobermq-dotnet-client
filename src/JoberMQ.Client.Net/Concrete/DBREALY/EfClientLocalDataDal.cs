using GenRep.EntityFramework;
using JoberMQ.Client.Net.Abstract.DBREALY;
using JoberMQ.Common.Dbos;
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