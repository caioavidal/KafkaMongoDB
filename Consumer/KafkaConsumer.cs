using Confluent.Kafka;
using Newtonsoft.Json;
using Persistence;
using Producer;

namespace Consumer
{
    public class KafkaConsumer
    {
        private readonly IMongoRepository<Stock> stockRepository;

        public KafkaConsumer(IMongoRepository<Stock> stockRepository)
        {
            this.stockRepository = stockRepository;
        }

        public async Task Receive()
        {
            var config = new ConsumerConfig
            {
                BootstrapServers = "localhost:9092",
                GroupId = "dotnet-consumer-group",
                AutoOffsetReset = AutoOffsetReset.Earliest,
                EnableAutoCommit = true
            };

            var cancelled = false;
            var cancellationToken = new CancellationTokenSource();

            var consumerBuilder = new ConsumerBuilder<Ignore, Stock>(config);
            consumerBuilder.SetValueDeserializer(new KafkaDeserializer<Stock>());

            using (var consumer = consumerBuilder.Build())
            {
                consumer.Subscribe("stocks");

                while (!cancelled)
                {
                    var consumeResult = consumer.Consume(cancellationToken.Token);
                    var stock = consumeResult.Message.Value;

                    await stockRepository.InsertOneAsync(stock);

                    Console.WriteLine($"Stock: {stock.Code} processed");
                }

                consumer.Close();
            }
        }
    }
}
