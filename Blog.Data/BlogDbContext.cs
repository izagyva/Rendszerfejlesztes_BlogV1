using Blog.Data.Entities;
using MongoDB.Driver;
using MongoDB.Bson;

namespace Blog.Data
{
    public class BlogMongoDbContext
    {
        private readonly IMongoDatabase _database;

        public BlogMongoDbContext(string connectionString)
        {
            var settings = MongoClientSettings.FromConnectionString(connectionString);
            settings.ServerApi = new ServerApi(ServerApiVersion.V1); // Set the ServerApi version

            var client = new MongoClient(settings);
            _database = client.GetDatabase("Blog"); // Use the "Blog" database
        }

        public IMongoCollection<User> Users => _database.GetCollection<User>("users");
        public IMongoCollection<Topic> Topics => _database.GetCollection<Topic>("topics");
        public IMongoCollection<FavoriteTopic> FavoriteTopics => _database.GetCollection<FavoriteTopic>("favorite_topics");
        public IMongoCollection<Topic_type> TopicTypes => _database.GetCollection<Topic_type>("topic_types");
        public IMongoCollection<Comment> Comments => _database.GetCollection<Comment>("comments");

        // ... other collections
    }
}
