using JoberMQ.Client.Common.Database.Base;

namespace JoberMQ.Client.Common.Dbos
{
    internal class JobDetailDbo : DboPropertyGuidBase, IDboBase
    {
        #region CONSTRUCTOR
        public JobDetailDbo()
        {
        }
        #endregion

        #region 11 - CLONE CREATED
        public Guid? CreatedJobDataDetailId { get; set; }
        #endregion

        #region 12 - CONSUMING
        public bool IsConsumingRetryPause { get; set; }
        public int ConsumingRetryMaxCount { get; set; }
        public int ConsumingRetryCounter { get; set; }
        #endregion

        #region 99 - CHILD PARENT
        //public JobDbo JobDbo { get; set; }
        public Guid? JobId { get; set; }
        #endregion
    }
}
