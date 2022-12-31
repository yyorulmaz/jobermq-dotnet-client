using JoberMQ.Client.Common.Database.Base;
using JoberMQ.Client.Common.Database.Enums;
using JoberMQ.Client.Common.Database.Helper;
using JoberMQ.Client.Common.Database.Repository.Abstraction.Mem;
using JoberMQ.Client.Common.Database.Repository.Abstraction.Opr;
using JoberMQ.Client.Common.Database.Repository.Abstraction.Text;

namespace JoberMQ.Common.Database.Repository.Implementation.Opr.Default
{
    internal class DfOprRepository<D> : IOprRepository<D>
        where D : DboPropertyGuidBase, new()
    {
        internal DfOprRepository(
            IMemRepository<Guid, D> dbMem,
            ITextRepository<D> dbText)
        {
            this.dbMem = dbMem;
            this.dbText = dbText;
        }
        internal DfOprRepository()
        {

        }

        //public IDbMemRepository<Guid, D> DbMem => dbMem;
        //public IDbTextRepository<D> DbText => dbText;
        private IMemRepository<Guid, D> dbMem;
        public IMemRepository<Guid, D> DbMem { get => dbMem; set => dbMem = value; }
        private ITextRepository<D> dbText;
        public ITextRepository<D> DbText { get => dbText; set => dbText = value; }

        #region CRUD
        public D Get(Guid id) => dbMem.Get(id);
        public List<D> GetAll(Func<D, bool> filter = null) => dbMem.GetAll(filter);
        public virtual bool Add(D dbo)
        {
            var processTime = DateHelper.GetUniversalNow();
            dbo.CreateDate = processTime;
            dbo.DataStatusType = DataStatusTypeEnum.Insert;
            dbo.ProcessTime = processTime;

            dbText.WriteLine(dbo);
            dbMem.Add(dbo.Id, dbo);
            ChangedAdded?.Invoke(dbo);

            return true;
        }
        public virtual bool Update(D dbo)
        {
            var processTime = DateHelper.GetUniversalNow();
            dbo.UpdateDate = processTime;
            dbo.DataStatusType = DataStatusTypeEnum.Update;
            dbo.ProcessTime = processTime;

            dbText.WriteLine(dbo);
            dbMem.Update(dbo.Id, dbo);
            ChangedUpdated?.Invoke(dbo);

            return true;
        }
        public virtual bool Delete(D dbo)
        {
            var processTime = DateHelper.GetUniversalNow();
            dbo.DataStatusType = DataStatusTypeEnum.Delete;
            dbo.ProcessTime = processTime;

            dbText.WriteLine(dbo);
            dbMem.Remove(dbo.Id);
            ChangedRemoved?.Invoke(dbo);

            return true;
        }

        public bool Commit(D dbo)
        {
            dbo.IsTransactionCompleted = true;
            dbo.TransactionDate = DateHelper.GetUniversalNow();

            return Add(dbo);
        }
        public bool Rollback(D dbo)
        {
            dbo.IsTransactionCompleted = false;
            dbo.TransactionDate = DateHelper.GetUniversalNow();

            return Delete(dbo);
        }
        #endregion

        #region Changed
        public event Action<D> ChangedAdded;
        public event Action<D> ChangedUpdated;
        public event Action<D> ChangedRemoved;
        #endregion

        public bool ImportTextDataToSetMemDb()
        {
            var datas = dbText.ReadAllDataGrouping(true);

            if (datas != null)
                foreach (var data in datas)
                    dbMem.Add(data.Id, data);

            return true;
        }

        public bool CreateDatabase()
            => dbText.CreateDatabase();
        public bool Setup()
            => dbText.Setup();

        public bool DataGroupingAndSize()
            => dbText.DataGroupingAndSize();

        public int ArsiveFileCounter { get => dbText.ArsiveFileCounter; set => dbText.ArsiveFileCounter = value; }

    }
}
