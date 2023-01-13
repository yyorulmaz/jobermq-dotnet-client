using JoberMQ.Common.Database.Base;
using JoberMQ.Common.Enums.Routing;
using JoberMQ.Common.Enums.Run;
using JoberMQ.Common.Enums.Timing;
using System;
using System.Collections.Generic;

namespace JoberMQ.Common.Dbos
{
    internal class JobDataDbo : DboPropertyGuidBase, IDboBase
    {
        #region CONSTRUCTOR
        public JobDataDbo()
        {
            Name = "";
            Description = "";
            GeneralData = "";
            ExecuteCountMax = 0;
            CreatedCount = 0;
            IsCountMax = false;
            IsTrigger = false;
            ErrorWorkflowStop = false;
            TriggerJobId = null;
            IsTriggerMain = false;
            ErrorMessageRoutingType = RoutingTypeEnum.None;
            IsCompleted = false;
            IsError = false;
            IsErrorMessageClientSend = false;
            Version = 1;
            TriggerGroupsId = null;
        }
        #endregion

        #region 1 - PRODUCER
        public string ProducerClientKey { get; set; }
        public string ProducerClientGroupKey { get; set; }
        #endregion

        #region 2 - RUN
        public RunTypeEnum RunType { get; set; }
        #endregion

        #region 3 - GENERAL
        public string Name { get; set; }
        public string Description { get; set; }
        public string GeneralData { get; set; }
        #endregion

        #region 4 - TIMING
        public TimingTypeEnum TimingType { get; set; }
        public ScheduleTypeEnum ScheduleType { get; set; }
        public string CronTime { get; set; }

        public int? ExecuteCountMax { get; set; }
        public int CreatedCount { get; set; }
        public bool IsCountMax { get; set; }
        #endregion

        #region 5 - TRIGGER
        public bool IsTrigger { get; set; }
        public bool ErrorWorkflowStop { get; set; }
        public Guid? TriggerJobId { get; set; }
        public bool IsTriggerMain { get; set; }
        #endregion

        #region 8 - CONSUMER ERROR
        public RoutingTypeEnum ErrorMessageRoutingType { get; set; }
        #endregion

        #region 9 - STATUS
        public bool IsCompleted { get; set; }
        public bool IsError { get; set; }
        public bool IsErrorMessageClientSend { get; set; }
        #endregion

        #region 10 - CLONE CREATED
        public int Version { get; set; }
        #endregion

        #region 12 - GROUP
        public Guid? TriggerGroupsId { get; set; }
        #endregion

        #region 99 - CHILD PARENT
        public ICollection<JobDataDetailDbo> Details { get; set; }
        #endregion
    }
}