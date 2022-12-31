using JoberMQ.Client.Common.Enums.Routing;
using JoberMQ.Client.Common.Enums.Run;
using JoberMQ.Client.Common.Enums.Timing;
using JoberMQ.Client.Common.Models.Message;
using JoberMQ.Client.Common.Models.Option;

namespace JoberMQ.Client.Common.Models.Builder
{
    public class JobBuilderModel
    {
        #region 1 - PRODUCER
        public string ProducerClientId { get; set; }
        public string ProducerClientGroupKey { get; set; }
        #endregion

        #region 2 - RUN
        public RunTypeEnum JobRunType { get; set; }
        #endregion

        #region 3 - GENERAL
        public OptionModel Option { get; set; }
        #endregion

        #region 4 - TIMING
        public TimingTypeEnum TimingType { get; set; }
        public ScheduleTypeEnum ScheduleType { get; set; }
        public string CronTime { get; set; }
        public int? DelayedSecond { get; set; }
        public int? ExecuteCountMax { get; set; }
        #endregion

        #region 5 - TRIGGER
        public bool ErrorWorkflowStop { get; set; } = false;
        public Guid? TriggerJobId { get; set; }
        //public bool IsTriggerMain { get; set; }
        #endregion

        #region 7 - CHILD
        public List<MultipleMethodModel> MultipleMethods { get; set; } = new List<MultipleMethodModel>();
        public List<MultipleMessageModel> MultipleMessages { get; set; } = new List<MultipleMessageModel>();
        #endregion

        #region 9 - CONSUMER ERROR
        public RoutingTypeEnum ErrorMessageRoutingType { get; set; }
        #endregion

        #region 13 - GROUP
        public Guid? TriggerGroupsId { get; set; }
        #endregion
    }
}
