using AutoMapper;
using Blog.Core.Models.Comments;
using Blog.Data;
using Blog.Data.Entities;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Blog.Core.Services
{
    public interface ICommentService
    {
        Task<CommentDto> GetComment(int id);
        Task<PagedResult<CommentDto>> GetCommentsByTopic(int topicId, int pageNumber, int pageSize);
        Task<CommentDto> CreateComment(CreateCommentDto createCommentDto);
        Task<CommentDto> UpdateComment(int id, CreateCommentDto updateCommentDto);
        Task DeleteComment(int id);
    }

    public class CommentService : ICommentService
    {
        private readonly BlogMongoDbContext _context;
        private readonly IMapper _mapper;

        public CommentService(BlogMongoDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<CommentDto> GetComment(int id)
        {
            var comment = await _context.Comments.Find(c => c.Id == id).FirstOrDefaultAsync();
            if (comment == null)
            {
                throw new System.Exception("Comment not found");
            }
            return _mapper.Map<CommentDto>(comment);
        }

        public async Task<PagedResult<CommentDto>> GetCommentsByTopic(int topicId, int pageNumber, int pageSize)
        {
            var filter = Builders<Comment>.Filter.Eq(c => c.TopicId, topicId);
            var comments = await _context.Comments.Find(filter)
                .Skip((pageNumber - 1) * pageSize)
                .Limit(pageSize)
                .ToListAsync();

            var totalRecords = await _context.Comments.CountDocumentsAsync(filter);
            var pagedResult = new PagedResult<CommentDto>
            {
                Items = _mapper.Map<List<CommentDto>>(comments),
                TotalRecords = (int)totalRecords,
                CurrentPage = pageNumber,
                PageSize = pageSize
            };
            return pagedResult;
        }

        public async Task<CommentDto> CreateComment(CreateCommentDto createCommentDto)
        {
            var comment = _mapper.Map<Comment>(createCommentDto);
            await _context.Comments.InsertOneAsync(comment);
            return _mapper.Map<CommentDto>(comment);
        }

        public async Task<CommentDto> UpdateComment(int id, CreateCommentDto updateCommentDto)
        {
            var comment = await _context.Comments.Find(c => c.Id == id).FirstOrDefaultAsync();
            if (comment == null)
            {
                throw new System.Exception("Comment not found");
            }
            _mapper.Map(updateCommentDto, comment);
            await _context.Comments.ReplaceOneAsync(c => c.Id == id, comment);
            return _mapper.Map<CommentDto>(comment);
        }

        public async Task DeleteComment(int id)
        {
            var result = await _context.Comments.DeleteOneAsync(c => c.Id == id);
            if (result.DeletedCount == 0)
            {
                throw new System.Exception("Comment not found");
            }
        }
    }
}
