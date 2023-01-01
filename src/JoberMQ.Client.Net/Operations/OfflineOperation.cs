using JoberMQ.Client.Net.Abstract.DBREALY;
using JoberMQ.Client.Net.Concrete.DBREALY.EF;
using JoberMQ.Client.Net.Database.Client;
using JoberMQ.Client.Net.Helpers;
using JoberMQ.Common.Database.Enums;
using JoberMQ.Common.Dbos;
using JoberMQ.Common.Enums.Data;
using JoberMQ.Common.Models.LocalData;
using Microsoft.Extensions.DependencyInjection;

namespace JoberMQ.Client.Net.Operations
{
    internal class OfflineOperation
    {
        internal static bool IsOfflineModeStarted = false;
        internal static bool IsOfflineModeData = false;
        internal static bool IsOfflineModeRuning = false;

        internal static ServiceProvider Provider;
        internal static ServiceCollection Collection;
        internal static IClientLocalDataDal ClientLocalDataDal;

        internal static bool DatabaseCreate()
        {
            ClientLocalSqliteDbContext.ConnectionString = "Data Source=LocalDb.db";
            return EntityFrameworkHelper.Migrate(new ClientLocalSqliteDbContext());
        }
        internal static bool DependencyCreate()
        {
            try
            {
                Collection = new ServiceCollection();
                Collection.AddSingleton<IClientLocalDataDal, EfClientLocalDataDal>(x => { return new EfClientLocalDataDal(() => new ClientLocalSqliteDbContext()); });
                Provider = Collection.BuildServiceProvider();
                ClientLocalDataDal = Provider.GetService<IClientLocalDataDal>();

                return true; 
            }
            catch (Exception)
            {
                return true;
            }
        }



        
        internal static async Task<LocalDataResponseModel> AddLocalData(PushDataTypeEnum pushDataType, Guid? jobId, string data)
        {
            var result = await ClientLocalDataDal.InsertAsync(new ClientLocalDataDbo
            {
                PushDataType = pushDataType,
                JobId = jobId,
                Data = data,
                DataStatusType = DataStatusTypeEnum.None
            });

            if (result != null)
            {
                return new LocalDataResponseModel
                {
                    IsOnline = false,
                    IsSuccess = true,
                    JobId = jobId,
                };
            }
            else
            {
                return new LocalDataResponseModel
                {
                    IsOnline = false,
                    IsSuccess = false,
                    JobId = null,
                };
            }
        }
        internal static async Task<List<ClientLocalDataDbo>> GetLocalDatas()
        {
            var data = await ClientLocalDataDal.SelectAllAsync();
            return data.OrderBy(o => o.PushDataType).ToList();
        }
        internal static async Task RemoveLocalData(ClientLocalDataDbo data)
        {
            await ClientLocalDataDal.DeleteAsync(data);
        }
    }
}
