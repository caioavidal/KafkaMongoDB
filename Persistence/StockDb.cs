using MongoDB.Driver;

namespace Persistence
{
    public class StockDb
    {
        public static IMongoDatabase Connect()
        {
            var connectionString = "mongodb://root:example@localhost:27017";
            var mongoClient = new MongoClient(connectionString);

           return mongoClient.GetDatabase(
                "stockdb");
        }

        public static async Task Save(Stock stock)
        {
            var db = Connect();
            await db.GetCollection<Stock>("stock").InsertOneAsync(stock);
        }

        public static async Task<IQueryable<Stock>> AsQueryable()
        {
            var db = Connect();
            return  db.GetCollection<Stock>("stock").AsQueryable<Stock>();
        }
    }
}
