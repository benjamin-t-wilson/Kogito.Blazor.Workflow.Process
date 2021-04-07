using Confluent.Kafka;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Kogito.Blazor.Workflow.Process.Interfaces
{
    public interface IKafkaFactory
    {
        IProducer<Null, string> CreateProducer();

        IProducer<Null, string> CreateProducer(string serverUrl);

        void ProduceMessage(IProducer<Null, string> producer, string topic, string message);

        void ProduceMessage(string topic, string message);

        Task CreateConsoleConsumer(string consumerGroup, string topic);

        Task CreateConsoleConsumer(string consumerGroup, string baseUrl, string topic);

        Task CreateConsumer(string consumerGroup, string topic, List<string> output);

        Task CreateConsumer(string consumerGroup, string baseUrl, string topic, List<string> output);
    }
}