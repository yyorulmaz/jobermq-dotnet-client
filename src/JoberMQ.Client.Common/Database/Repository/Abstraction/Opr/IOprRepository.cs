using JoberMQ.Client.Common.Database.Base;
using JoberMQ.Client.Common.Database.Repository.Abstraction.Mem;
using JoberMQ.Client.Common.Database.Repository.Abstraction.Text;
using System;
using System.Collections.Generic;

namespace JoberMQ.Client.Common.Database.Repository.Abstraction.Opr
{
    internal interface IOprRepository<D>
    where D : DboPropertyGuidBase, new()
    {
        public IMemRepository<Guid, D> DbMem { get; set; }
        public ITextRepository<D> DbText { get; set; }


        #region CRUD
        public D Get(Guid id);
        public List<D> GetAll(Func<D, bool> filter = null);
        public bool Add(D dbo);
        public bool Update(D dbo);
        public bool Delete(D dbo);

        public bool Commit(D dbo);
        public bool Rollback(D dbo);
        #endregion

        #region Changed
        public event Action<D> ChangedAdded;
        public event Action<D> ChangedUpdated;
        public event Action<D> ChangedRemoved;
        #endregion

        public bool ImportTextDataToSetMemDb();

        public bool CreateDatabase();
        public bool Setup();
        public bool DataGroupingAndSize();
        int ArsiveFileCounter { get; set; }
    }
}
