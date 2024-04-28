using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

public class Comment
{

}
namespace Blog.Core.Services
{
    public interface ICommentService
    {
        Task<List<Comment>> AddComment(int topicId, string text);
        Task<List<Comment>> GetCommentsForTopic(int id);
    }

    public class CommentService : ICommentService
    {
        public async Task<List<Comment>> AddComment(int topicId, string text)
        {
            return new List<Comment>();
        }

        public async Task<List<Comment>> GetCommentsForTopic(int id)
        {
            return new List<Comment>();
        }
    }
}
