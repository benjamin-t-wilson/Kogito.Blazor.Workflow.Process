using Confluent.Kafka;
using Microsoft.Extensions.Configuration;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Kogito.Blazor.Workflow.Process.Factories
{
    public class KafkaFactory
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
            var conf = new ProducerConfig { BootstrapServers = "http://" + _baseUrl };

            return new ProducerBuilder<Null, string>(conf).Build();
        }

        public IProducer<Null, string> CreateProducer(string serverUrl)
        {
            var conf = new ProducerConfig { BootstrapServers = serverUrl };

            return new ProducerBuilder<Null, string>(conf).Build();
        }

        public void ProduceMessage(IProducer<Null, string> producer, string topic, string message)
        {
            producer.Produce(topic, new Message<Null, string> { Value = message }, Handler);
            producer.Flush(TimeSpan.FromSeconds(10));
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

        public Task CreateConsumer(string consumerGroup, string topic)
        {
            var conf = new ConsumerConfig
            {
                GroupId = consumerGroup,
                BootstrapServers = "http://" + _baseUrl,
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
                        Console.WriteLine($"Consumed message '{cr.Message.Value}' at: '{cr.TopicPartitionOffset}'.");
                    }
                    catch (ConsumeException e)
                    {
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

        public Task CreateConsumer(string consumerGroup, string baseUrl, string topic)
        {
            var conf = new ConsumerConfig
            {
                GroupId = consumerGroup,
                BootstrapServers = baseUrl,
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
                        Console.WriteLine($"Consumed message '{cr.Message.Value}' at: '{cr.TopicPartitionOffset}'.");
                    }
                    catch (ConsumeException e)
                    {
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