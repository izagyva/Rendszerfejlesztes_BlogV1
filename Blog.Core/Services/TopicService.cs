using AutoMapper;
using Blog.Core.Models.Topics;
using Blog.Data;
using Blog.Data.Entities;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Blog.Core.Services
{
    public interface ITopicService
    {
        Task<TopicDto> GetTopic(int id);
        Task<PagedResult<TopicDto>> GetTopics(int pageNumber, int pageSize);
        Task<PagedResult<TopicDto>> CreateTopic(CreateTopicDto createTopicDto);
        Task<PagedResult<TopicDto>> UpdateTopic(int id, CreateTopicDto updateTopicDto);
        Task<PagedResult<TopicDto>> DeleteTopic(int id);
    }

    public class TopicService : ITopicService
    {
        private readonly BlogMongoDbContext _context;
        private readonly IMapper _mapper;

        public TopicService(BlogMongoDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<TopicDto> GetTopic(int id)
        {
            var topic = await _context.Topics.Find(t => t.Id == id).FirstOrDefaultAsync();
            if (topic == null)
            {
                throw new System.Exception("Topic not found");
            }
            return _mapper.Map<TopicDto>(topic);
        }

        public async Task<PagedResult<TopicDto>> GetTopics(int pageNumber, int pageSize)
        {
            var topics = await _context.Topics.Find(_ => true)
                .Skip((pageNumber - 1) * pageSize)
                .Limit(pageSize)
                .ToListAsync();

            var totalRecords = await _context.Topics.CountDocumentsAsync(_ => true);
            var pagedResult = new PagedResult<TopicDto>
            {
                Items = _mapper.Map<List<TopicDto>>(topics),
                TotalRecords = (int)totalRecords,
                CurrentPage = pageNumber,
                PageSize = pageSize
            };
            return pagedResult;
        }

        public async Task<PagedResult<TopicDto>> CreateTopic(CreateTopicDto createTopicDto)
        {
            var topic = _mapper.Map<Topic>(createTopicDto);
            await _context.Topics.InsertOneAsync(topic);
            return await GetTopics(1, 10); // Assuming you want to return the first page of topics
        }

        public async Task<PagedResult<TopicDto>> UpdateTopic(int id, CreateTopicDto updateTopicDto)
        {
            var topic = await _context.Topics.Find(t => t.Id == id).FirstOrDefaultAsync();
            if (topic == null)
            {
                throw new System.Exception("Topic not found");
            }
            _mapper.Map(updateTopicDto, topic);
            await _context.Topics.ReplaceOneAsync(t => t.Id == id, topic);
            return await GetTopics(1, 10); // Assuming you want to return the first page of topics
        }

        public async Task<PagedResult<TopicDto>> DeleteTopic(int id)
        {
            var result = await _context.Topics.DeleteOneAsync(t => t.Id == id);
            if (result.DeletedCount == 0)
            {
                throw new System.Exception("Topic not found");
            }
            return await GetTopics(1, 10); // Assuming you want to return the first page of topics
        }
    }
}
