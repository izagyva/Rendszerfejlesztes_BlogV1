using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.Collections.Generic;

namespace Blog.Data.Entities
{
    public class Topic
    {
        [BsonId]
        [BsonRepresentation(BsonType.Int32)]
        public int Id { get; set; }

        [BsonElement("name")]
        public string Name { get; set; }

        [BsonElement("type_id")]
        [BsonRepresentation(BsonType.Int32)]
        public int TypeId { get; set; }

        [BsonElement("description")]
        public string Description { get; set; }

        [BsonElement("comments")]
        [BsonIgnoreIfNull]
        public List<ObjectId> Comments { get; set; } = new List<ObjectId>();
    }
}
