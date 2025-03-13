using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace BalanceLedgerApi.Domain.Model
{
    public class BaseMongo
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }
    }
}
