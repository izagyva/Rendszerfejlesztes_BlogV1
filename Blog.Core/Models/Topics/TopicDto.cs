using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Core.Models.Topics
{
    public class TopicDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public TopicTypeDto Type { get; set; }
    }

    public class CreateTopicDto
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public int TypeId { get; set; }
    }

    public class TopicTypeDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}