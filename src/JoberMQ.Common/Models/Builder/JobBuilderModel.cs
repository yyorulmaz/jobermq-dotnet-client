﻿using JoberMQ.Common.Enums.Routing;
using JoberMQ.Common.Enums.Publisher;
using JoberMQ.Common.Enums.Timing;
using JoberMQ.Common.Models.Message;
using JoberMQ.Common.Models.Option;
using System;
using System.Collections.Generic;

namespace JoberMQ.Common.Models.Builder
{
    public class JobBuilderModel
    {
        #region 1 - PRODUCER
        public string ProducerClientKey { get; set; }
        public string ProducerClientGroupKey { get; set; }
        #endregion

        #region 2 - RUN
        public PublisherTypeEnum PublisherType { get; set; }
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
