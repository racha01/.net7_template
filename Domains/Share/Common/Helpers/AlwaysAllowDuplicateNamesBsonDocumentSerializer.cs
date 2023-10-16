using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;

namespace Common.Helpers
{
    public class AlwaysAllowDuplicateNamesBsonDocumentSerializer : BsonDocumentSerializer
    {
        protected override BsonDocument DeserializeValue(BsonDeserializationContext context, BsonDeserializationArgs args)
        {
            if (!context.AllowDuplicateElementNames)
                context = context.With(c => c.AllowDuplicateElementNames = true);
            return base.DeserializeValue(context, args);
        }

        public override BsonDocument Deserialize(BsonDeserializationContext context, BsonDeserializationArgs args)
        {
            if (!context.AllowDuplicateElementNames)
                context = context.With(c => c.AllowDuplicateElementNames = true);
            return base.Deserialize(context, args);
        }
    }
}
