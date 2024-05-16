using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.Collections.Generic;

namespace Blog.Data.Entities
{
    public class User
    {
        [BsonId]
        [BsonRepresentation(BsonType.Int32)]
        public int Id { get; set; }

        [BsonElement("username")]
        public string username { get; set; }

        [BsonElement("name")]
        public string name { get; set; }

        [BsonElement("password")]
        public string password { get; set; }

        [BsonElement("roles")]
        public List<string> roles { get; set; } = new List<string>();

        [BsonElement("comments")]
        public List<ObjectId> comments { get; set; } = new List<ObjectId>();

        [BsonElement("favoriteTopics")]
        public List<ObjectId> favoritetopics { get; set; } = new List<ObjectId>();

        public User()
        {
            // Default constructor logic (if any)
        }

        public User(string username)
        {
            username = username;
            SetRoleBasedOnUsername();
        }

        public void SetRoleBasedOnUsername()
        {
            if (username.ToLower() == "admin")
            {
                roles.Add("Admin");
            }
            else
            {
                roles.Add("User");
            }
        }
    }
}
