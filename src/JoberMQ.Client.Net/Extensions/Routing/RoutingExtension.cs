using JoberMQ.Client.Net.Abstraction.Client;
using JoberMQ.Client.Net.Constants;
using JoberMQ.Library.Enums.Routing;
using JoberMQ.Library.Models.Routing;
using System.ComponentModel;
using System;

namespace JoberMQ.Client.Net.Extensions.Routing
{
    public static class RoutingExtension
    {
        public static RoutingSpecialModel CreateRoutingSpecial(this IClient client, string clientKey)
            => CreateRoutingSpecial(clientKey);
        static RoutingSpecialModel CreateRoutingSpecial(string clientKey)
            => new RoutingSpecialModel
            {
                ClientKey = clientKey
            };
        internal static RoutingModel CreateRouting(this IClient client, RoutingSpecialModel routingSpecial)
            => new RoutingModel
            {
                DistributorKey = ClientConst.DistributorDefaultDirectKey,
                QueueKey = ClientConst.QueueDefaultSpecialFifoKey,
                ClientKey = routingSpecial.ClientKey,
                RoutingType = RoutingTypeEnum.Special
            };






        public static RoutingQueueModel CreateRoutingQueue(this IClient client, string queueKey)
            => CreateRoutingQueue(queueKey);
        static RoutingQueueModel CreateRoutingQueue(string queueKey)
            => new RoutingQueueModel
            {
                QueueKey = queueKey,
            };
        internal static RoutingModel CreateRouting(this IClient client, RoutingQueueModel routingQueue)
            => new RoutingModel
            {
                DistributorKey = ClientConst.DistributorDefaultDirectKey,
                QueueKey = routingQueue.QueueKey,
                RoutingType = RoutingTypeEnum.Queue
            };


        public static RoutingEventModel CreateRoutingEvent(this IClient client, string eventName)
            => CreateRoutingEvent(eventName);
        static RoutingEventModel CreateRoutingEvent(string eventName)
            => new RoutingEventModel
            {
                EventName = eventName,
            };
        internal static RoutingModel CreateRouting(this IClient client, RoutingEventModel routingEvent)
            => new RoutingModel
            {
                DistributorKey = ClientConst.DistributorDefaultEventKey,
                EventName = routingEvent.EventName,
                QueueKey = routingEvent.EventName,
                RoutingType = RoutingTypeEnum.Event
            };



        // routingKey kuyruğu bulmak için yardımcı değer
        // distributor type - Direct => routingKey 'i queueKey değerine atar
        // distributor type - Filter => 
        // distributor type - Event  => 

        public static RoutingFilterModel CreateRoutingFilter(this IClient client, string filterRegex)
            => CreateRoutingFilter(filterRegex);
        public static RoutingFilterModel CreateRoutingFilter(this IClient client, string startsWith, string[] contains, string endsWith)
            => CreateRoutingFilter(startsWith, contains, endsWith);
        static RoutingFilterModel CreateRoutingFilter(string filterRegex)
            => new RoutingFilterModel
            {
                FilterRegex = filterRegex
            };
        static RoutingFilterModel CreateRoutingFilter(string startsWith, string[] contains, string endsWith)
            => new RoutingFilterModel
            {
                StartsWith = startsWith,
                Contains = contains,
                EndsWith = endsWith
            };
        internal static RoutingModel CreateRouting(this IClient client, RoutingFilterModel routingFilter)
        {
            var routing = new RoutingModel();
            routing.DistributorKey = ClientConst.DistributorDefaultFilterKey;
            routing.FilterRegex = routingFilter.FilterRegex;
            routing.StartsWith = routingFilter.StartsWith;
            routing.Contains = routingFilter.Contains;
            routing.EndsWith = routingFilter.EndsWith;
            routing.RoutingType = RoutingTypeEnum.Filter;

            if (routingFilter.FilterRegex == null)
            {
                string delimiter = "|";
                string containsValue = String.Join(delimiter, routingFilter.Contains);
                routing.FilterRegex = $@"^{routingFilter.StartsWith}(.*?)({containsValue})+.*?{routingFilter.EndsWith}";
            }

            return routing;
        }

    }
}
