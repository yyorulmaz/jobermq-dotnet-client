using JoberMQ.Client.Common.Database.Base;

namespace JoberMQ.Client.Common.Dbos
{
    internal class JobDbo : DboPropertyGuidBase, IDboBase
    {
        #region CONSTRUCTOR
        public JobDbo()
        {
            Version = 1;
            TriggerGroupsId = null;
            IsCompleted = false;
            IsError = false;
            TriggerGroupsId = null;
        }
        #endregion

        #region 5 - TRIGGER
        public bool IsTrigger { get; set; }
        public bool ErrorWorkflowStop { get; set; }
        public Guid? TriggerJobId { get; set; }
        public bool IsTriggerMain { get; set; }
        #endregion

        #region 9 - STATUS
        public bool IsCompleted { get; set; }
        public bool IsError { get; set; }
        #endregion

        #region 10 - CLONE CREATED
        public int Version { get; set; }
        public Guid CreatedJobDataId { get; set; }
        #endregion

        #region 12 - GROUP
        public Guid? TriggerGroupsId { get; set; }
        #endregion

        #region 99 - CHILD PARENT
        public ICollection<JobDetailDbo> Details { get; set; }
        #endregion
    }
}
