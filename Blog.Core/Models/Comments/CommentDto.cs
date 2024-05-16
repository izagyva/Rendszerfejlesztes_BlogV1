using Blog.Core.Models.Topics;
using Blog.Core.Models.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Core.Models.Comments
{
    public class CommentDto
    {
        public int Id { get; set; }
        public string Body { get; set; }
        public DateTime Timestamp { get; set; }
        public UserDto User { get; set; }
        public TopicDto Topic { get; set; }
    }

    public class CreateCommentDto
    {
        public string Body { get; set; }
        public int UserId { get; set; }
        public int TopicId { get; set; }
    }
}