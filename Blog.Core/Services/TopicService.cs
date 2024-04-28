using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

public class Topic
{
}
namespace Blog.Core.Services
{
    public interface ITopicService
    {
        //Task GetTopics(int pageNumber, int pageSize);
        //Task GetTopic(int id);
        //Task GetTopicsByUser(int userId);
        Task<List<Topic>> GetTopics(int pageNumber, int pageSize);
        Task<List<Topic>> GetTopicsByUser(int userId);
        Task<Topic> GetTopic(int id);
    }

    public class TopicService : ITopicService
    {
        //public async Task GetTopics(int pageNumber, int pageSize)
        //{

        //}

        //public async Task GetTopic(int id)
        //{

        //}
        //public async Task GetTopicsByUser(int userId)
        //{

        //}
        public async Task<List<Topic>> GetTopics(int pageNumber, int pageSize)
        {
            return new List<Topic>(); // Placeholder value
        }
        public async Task<Topic> GetTopic(int id)
        {
            Topic asd = new Topic();
            return asd; // Placeholder value
        }

        public async Task<List<Topic>> GetTopicsByUser(int userId)
        {
            return new List<Topic>(); // Placeholder value
        }

    }
}
