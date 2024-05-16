using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.Collections.Generic;

namespace Blog.Data.Entities
{
    public class Topic_type
    {
        [BsonId]
        [BsonRepresentation(BsonType.Int32)]
        public int Id { get; set; }

        [BsonElement("name")]
        public string Name { get; set; }

        [BsonIgnoreIfNull]
        public List<ObjectId> Topics { get; set; } = new List<ObjectId>();
    }
}
