using JoberMQ.Client.Common.Database.Base;

namespace JoberMQ.Client.Common.Dbos
{
    public class JobMessageLogDbo : DboPropertyGuidBase, IDboBase
    {
        #region CONSTRUCTOR
        public JobMessageLogDbo()
        {
            IsError = false;
        }
        #endregion

        #region 9 - STATUS
        public bool IsError { get; set; }
        #endregion

        #region 13 - MESSAGE INFO
        public string Message { get; set; }
        public DateTime? MessageDate { get; set; }
        #endregion

        #region 99 - CHILD PARENT
        public Guid? JobMessageId { get; set; }
        #endregion
    }
}
