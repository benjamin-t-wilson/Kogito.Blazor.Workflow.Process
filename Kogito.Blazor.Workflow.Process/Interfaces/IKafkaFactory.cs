using Confluent.Kafka;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Kogito.Blazor.Workflow.Process.Interfaces
{
    public interface IKafkaFactory
    {
        IProducer<Null, string> CreateProducer();

        void ProduceMessage(string topic, string message);

        Task CreateConsumer(string consumerGroup, string topic, List<string> output, Action action = null);
    }
}