using Confluent.Kafka;
using Confluent.Kafka.Serialization;
using System;
using System.Collections.Generic;
using System.Text;

namespace KafkaProducer
{
    class Program
    {
        static void Main(string[] args)
        {
            var config = new Dictionary<string, object>
            {
                { "bootstrap.servers", "localhost:9092" }
            };

            using (var producer = new Producer<Null, string>(config, null, new StringSerializer(Encoding.UTF8)))
            {
                string text = null;

                while (text != "quit")
                {
                    Console.Write("Add Message: ");
                    text = Console.ReadLine();
                    producer.ProduceAsync("topic-royal-2", null, text);
                }
                producer.Flush(100);
            }
        }
    }
}
