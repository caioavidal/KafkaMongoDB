using Confluent.Kafka;
using Newtonsoft.Json;
using Persistence;

namespace Producer
{
    public class KafkaProducer
    {
        public static void handler(DeliveryReport<Null, string> result)
        {
            if (result.Error.IsError)
            {
                Console.WriteLine(result.Error.Reason);
                return;
            }

            var stock = JsonConvert.DeserializeObject<Stock>(result.Message.Value);
            if (stock == null) return;

            Console.WriteLine($"Stock {stock.Code} sent");
        }

        public void Send()
        {
            var config = new ProducerConfig
            {
                BootstrapServers = "localhost:9092",
                EnableDeliveryReports = true,
                LogConnectionClose = false,
            };
            var producerBuilder = new ProducerBuilder<Null, string>(config);
            producerBuilder.SetLogHandler((_,_) => { });

            using (var producer = producerBuilder.Build())
            {
                for (int i = 0; i < 10; i++)
                {
                    producer.Produce("stocks", new Message<Null, string>()
                    {
                        Value = JsonConvert.SerializeObject(Stock.CreateRandomStock())
                    }, handler);
                }

                producer.Flush(TimeSpan.FromSeconds(10));
            }
        }
    }
}
