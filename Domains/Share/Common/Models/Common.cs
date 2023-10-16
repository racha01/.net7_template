using MongoDB.Bson.Serialization.Attributes;

namespace Common.Models
{
    [BsonIgnoreExtraElements]
    public class CreateInfoSimply
    {
        public DateTime timestamp { get; set; }
        public string user_id { get; set; }
        public string user_name { get; set; }
    }
}
