using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace BalanceLedgerApi.Domain.Model
{
    public class BaseMongo
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }
    }
}
