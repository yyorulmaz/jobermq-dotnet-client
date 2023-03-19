using JoberMQ.Client.Net.Models.DeclareConsume;

namespace JoberMQ.Client.Net.Extensions.DeclareConsume
{
    public static class DeclareConsumeBuildExtension
    {
        public static DeclareConsumeTransportModel Build(this DeclareConsumeSpecialModel declareConsumeSpecial)
            => declareConsumeSpecial.DeclareConsumeTransport;
        public static DeclareConsumeTransportModel Build(this DeclareConsumeQueueModel declareConsumeQueue)
            => declareConsumeQueue.DeclareConsumeTransport;
        public static DeclareConsumeTransportModel Build(this DeclareConsumeGroupModel declareConsumeGroup)
            => declareConsumeGroup.DeclareConsumeTransport;
        public static DeclareConsumeTransportModel Build(this DeclareConsumeEventModel declareConsumeEvent)
            => declareConsumeEvent.DeclareConsumeTransport;
    }
}
