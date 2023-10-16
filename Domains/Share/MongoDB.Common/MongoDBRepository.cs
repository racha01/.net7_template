using MongoDB.Bson;
using MongoDB.Driver;

namespace MongoDB.Common
{
    public interface IMongoDBRepository
    {
        Task CreateCollectionAsync(string collectionName);
        Task DropCollectionAsync(string collectionName);
        Task<bool> ExistCollectionAsync(string collectionName);
        IMongoCollection<BsonDocument> GetCollection(string collectionName);
    }
    public class MongoDBRepository : IMongoDBRepository
    {
        private readonly IMongoDatabase mongoDatabase;
        public MongoDBRepository(DatabaseContext context)
        {
            mongoDatabase = context.MongoDatabase;
        }

        public async Task DropCollectionAsync(string collectionName)
        {
            string clName = collectionName.ToLower().StartsWith("cl_") ? collectionName.ToLower().Replace("cl_", "") : collectionName.ToLower();
            await mongoDatabase.DropCollectionAsync(clName);
        }
        public async Task<bool> ExistCollectionAsync(string collectionName)
        {
            string clName = collectionName.ToLower().StartsWith("cl_") ? collectionName.ToLower().Replace("cl_", "") : collectionName.ToLower();
            var filter = new BsonDocument("name", clName);
            //filter by collect-ion name
            //var cc = await mongoDatabase.ListCollectionsAsync();
            var collections = await mongoDatabase.ListCollectionsAsync(new ListCollectionsOptions { Filter = filter });

            //var x = collections.ToListAsync();
            //check for existence
            return await collections.AnyAsync();
        }
        public async Task CreateCollectionAsync(string collectionName)
        {
            string clName = collectionName.ToLower().StartsWith("cl_") ? collectionName.ToLower().Replace("cl_", "") : collectionName.ToLower();
            await mongoDatabase.CreateCollectionAsync(clName);
        }

        public IMongoCollection<BsonDocument> GetCollection(string collectionName)
        {
            string clName = collectionName.ToLower().StartsWith("cl_") ? collectionName.ToLower().Replace("cl_", "") : collectionName.ToLower();
            return mongoDatabase.GetCollection<BsonDocument>(clName);
        }
    }
}
