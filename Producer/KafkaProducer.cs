using Confluent.Kafka;
using Confluent.SchemaRegistry;
using MongoDB.Bson.Serialization.Serializers;
using Newtonsoft.Json;
using Persistence;
using System.Text;

namespace Producer
{
    public class KafkaProducer
    {
        public static void handler(DeliveryReport<string, Stock> result)
        {
            if (result.Error.IsError)
            {
                Console.WriteLine(result.Error.Reason);
                return;
            }

            var stock = result.Message.Value;

            if (stock is null) return;

            Console.WriteLine($"Stock {stock.Code} sent");
        }

        public void Send()
        {
            var config = new ProducerConfig
            {
                BootstrapServers = "localhost:9092"
            };


            var producerBuilder = new ProducerBuilder<string, Stock>(config);
            producerBuilder.SetLogHandler((_,_) => { });
      
            producerBuilder.SetValueSerializer(new KafkaSerializer<Stock>());

            using (var producer = producerBuilder.Build())
            {
                for (int i = 0; i < 10; i++)
                {
                    var stock = Stock.CreateRandomStock();

                    producer.Produce("stocks", new Message<string, Stock>()
                    {
                        Key = stock.Code,
                        Value = stock
                    }, handler);
                }

                producer.Flush(TimeSpan.FromSeconds(10));
            }
        }
    }


}
