using Blog.Core.Models.Topics;
using Blog.Core.Models.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Core.Models.FavoriteTopics
{
    public class FavoriteTopicDto
    {
        public int UserId { get; set; }
        public int TopicId { get; set; }
        public UserDto User { get; set; }
        public TopicDto Topic { get; set; }
    }
}