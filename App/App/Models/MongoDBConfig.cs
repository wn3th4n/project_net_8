using Microsoft.Extensions.Configuration;
using MongoDB.Driver;

namespace IdentityAPI.Models
{
    public class MongoDbConfig
    {
        private readonly IMongoDatabase _database;

        public MongoDbConfig(IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("connectionString");
            var databaseName = configuration.GetValue<string>("database");

            var client = new MongoClient(connectionString);
            _database = client.GetDatabase(databaseName);
        }

        public IMongoCollection<T> GetCollection<T>(string collectionName)
        {
            return _database.GetCollection<T>(collectionName);
        }
    }
}