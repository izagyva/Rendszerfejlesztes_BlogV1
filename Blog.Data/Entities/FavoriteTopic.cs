using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Blog.Data.Entities
{
    public class FavoriteTopic
    {
        [BsonId]
        [BsonRepresentation(BsonType.Int32)]
        public int Id { get; set; }

        [BsonElement("user_id")]
        [BsonRepresentation(BsonType.Int32)]
        public int UserId { get; set; }

        [BsonElement("topic_id")]
        [BsonRepresentation(BsonType.Int32)]
        public int TopicId { get; set; }

    }
}
