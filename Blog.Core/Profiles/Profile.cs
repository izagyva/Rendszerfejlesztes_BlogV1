using AutoMapper;
using Blog.Core.Models.Comments;
using Blog.Core.Models.Topics;
using Blog.Core.Models.Users;
using Blog.Data.Entities;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Blog.Core.Profiles
{
    public class CommentMapperConfig : Profile
    {
        public CommentMapperConfig()
        {
            CreateMap<Comment, CommentDto>();
            CreateMap<CreateCommentDto, Comment>();
        }
    }

    public class TopicMapperConfig : Profile
    {
        public TopicMapperConfig()
        {
            CreateMap<Topic, TopicDto>()
                // Remove or modify this line to map the TypeId to the Type property in TopicDto
                // .ForMember(dest => dest.Type, opt => opt.MapFrom(src => src.TopicType));
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.TypeId));
            CreateMap<CreateTopicDto, Data.Entities.Topic>();
            CreateMap<Data.Entities.Topic_type, Models.Topics.TopicTypeDto>();
        }
    }

    public class UserMapperConfig : Profile
    {
        public UserMapperConfig()
        {
            CreateMap<User, UserDto>();
            // Add any additional mappings if needed
        }
    }
}
