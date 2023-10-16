
using MongoDB.Driver;

namespace MongoDB.Common
{
    public class DatabaseContext : MongoDBContext
    {
        public DatabaseContext(string connectionString, string databaseName, bool isSSL) : base(connectionString, databaseName, isSSL)
        {
        }
    }

    public abstract class MongoDBContext : IMongoDBContext
    {
        public IMongoDatabase MongoDatabase { get; }
        public IMongoClient MongoClient { get; }
        public MongoDBContext(string connectionString, string databaseName, bool isSSL)
        {
            try
            {
                MongoClientSettings settings = MongoClientSettings.FromUrl(new MongoUrl(connectionString));
                if (isSSL)
                {
                    settings.SslSettings = new SslSettings { EnabledSslProtocols = System.Security.Authentication.SslProtocols.Tls12 };
                }

                MongoClient = new MongoClient(settings);
                MongoDatabase = MongoClient.GetDatabase(databaseName);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }

    public interface IMongoDBContext
    {
        IMongoDatabase MongoDatabase { get; }
        IMongoClient MongoClient { get; }
    }
}
