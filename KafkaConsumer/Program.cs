using Confluent.Kafka;
using Confluent.Kafka.Serialization;
using System;
using System.Collections.Generic;
using System.Text;

namespace KafkaConsumer
{
    class Program
    {
        static void Main(string[] args)
        {
            var config = new Dictionary<string, object>
              {
                  { "group.id", "messageConsumer" }
                  ,{ "bootstrap.servers", "localhost:9092" }
                  ,{ "enable.auto.commit", "false"}
              };

            using (var consumer = new Consumer<Null, string>(config, null, new StringDeserializer(Encoding.UTF8)))
            {
                consumer.Subscribe(new string[] { "topic-royal-2" });

                consumer.OnMessage += (_, msg) =>
                {
                    Console.WriteLine($"Message: {msg.Topic} Partition: {msg.Partition} Offset: {msg.Offset} {msg.Value}");
                    consumer.CommitAsync(msg);
                };

                while (true)
                {
                    consumer.Poll(100);
                }
            }
        }
    }
}
