using JoberMQ.Library.Models.Consume;

namespace JoberMQ.Client.Net.Extensions.Consume
{
    public static class ConsumeBuildExtension
    {
        public static ConsumeTransportModel Build(this ConsumeSpecialModel consumeSpecial)
            => consumeSpecial.ConsumeTransport;
        public static ConsumeTransportModel Build(this ConsumeQueueModel consumeQueue)
            => consumeQueue.ConsumeTransport;
        public static ConsumeTransportModel Build(this ConsumeGroupModel consumeGroup)
            => consumeGroup.ConsumeTransport;
        public static ConsumeTransportModel Build(this ConsumeEventModel consumeEvent)
            => consumeEvent.ConsumeTransport;
    }
}
