using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;

namespace Blog.Data.Entities
{
    public class Comment
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

        [BsonElement("body")]
        public string Body { get; set; }

        [BsonElement("timestamp")]
        public string Timestamp { get; set; }

    }
}
