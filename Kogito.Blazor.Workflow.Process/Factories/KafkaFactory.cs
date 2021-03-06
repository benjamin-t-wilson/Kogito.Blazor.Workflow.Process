using Confluent.Kafka;
using Kogito.Blazor.Workflow.Process.Interfaces;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Kogito.Blazor.Workflow.Process.Factories
{
    public class KafkaFactory : IKafkaFactory
    {
        private readonly IConfiguration _config;
        private readonly string _baseUrl;

        public KafkaFactory(IConfiguration config)
        {
            _config = config;
            _baseUrl = _config.GetValue<string>("Kafka_BaseUrl");
        }

        public IProducer<Null, string> CreateProducer()
        {
            var conf = new ProducerConfig { BootstrapServers = _baseUrl };

            return new ProducerBuilder<Null, string>(conf).Build();
        }

        public void ProduceMessage(string topic, string message)
        {
            var producer = CreateProducer();
            producer.Produce(topic, new Message<Null, string> { Value = message }, Handler);
            producer.Flush(TimeSpan.FromSeconds(10));
        }

        private static void Handler(DeliveryReport<Null, string> r) =>
            Console.WriteLine(!r.Error.IsError
                ? $"Delivered message to {r.TopicPartitionOffset}"
                : $"Delivery Error: {r.Error.Reason}");

        public Task CreateConsumer(string consumerGroup, string topic, List<string> output, Action action = null)
        {
            var conf = new ConsumerConfig
            {
                GroupId = consumerGroup,
                BootstrapServers = _baseUrl,
                // Note: The AutoOffsetReset property determines the start offset in the event
                // there are not yet any committed offsets for the consumer group for the
                // topic/partitions of interest. By default, offsets are committed
                // automatically, so in this example, consumption will only start from the
                // earliest message in the topic 'my-topic' the first time you run the program.
                AutoOffsetReset = AutoOffsetReset.Earliest
            };

            using var c = new ConsumerBuilder<Ignore, string>(conf).Build();
            c.Subscribe(topic);

            CancellationTokenSource cts = new CancellationTokenSource();
            Console.CancelKeyPress += (_, e) =>
            {
                e.Cancel = true; // prevent the process from terminating.
                cts.Cancel();
            };

            try
            {
                while (true)
                {
                    try
                    {
                        var cr = c.Consume(cts.Token);
                        output.Add($"Consumed message '{cr.Message.Value}' at: '{cr.TopicPartitionOffset}'.");
                        Console.WriteLine($"Consumed message '{cr.Message.Value}' at: '{cr.TopicPartitionOffset}'.");
                        
                        if(action != null)
                        {
                            Console.WriteLine("Invoking action");
                            action.Invoke();
                        }
                    }
                    catch (ConsumeException e)
                    {
                        output.Add($"Error occured: {e.Error.Reason}");
                        Console.WriteLine($"Error occured: {e.Error.Reason}");
                    }
                }
            }
            catch (OperationCanceledException)
            {
                // Ensure the consumer leaves the group cleanly and final offsets are committed.
                c.Close();
            }

            return Task.CompletedTask;
        }
    }
}