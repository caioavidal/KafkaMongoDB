using Consumer;
using Persistence;

Console.WriteLine("#### Consumer ####");

var mongoSettings = new MongoDbSettings
{
    DatabaseName = "stockdb",
    ConnectionString = "mongodb://root:example@localhost:27017"
};
var mongoRepository = new MongoRepository<Stock>(mongoSettings);
var consumer = new KafkaConsumer(mongoRepository);

await consumer.Receive();
