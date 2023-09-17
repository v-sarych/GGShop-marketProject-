using AutoMapper;
using AutoMapper.QueryableExtensions;
using ShopApi.Model.Exceptions;
using Microsoft.EntityFrameworkCore;
using Npgsql.Replication.PgOutput.Messages;
using ShopApi.Model.Entities.DTO.Comment;
using ShopApi.Model.Interfaces.Repository;
using ShopDb;
using ShopDb.Entities;

namespace ShopApi.Model.Repositories
{
    public class CommentRepository : ICommentRepository
    {
        private readonly IShopDbContext _dbContext;
        private readonly IMapper _mapper;

        public CommentRepository(IShopDbContext dbContext, IMapper mapper) => (_dbContext, mapper) = (dbContext, _mapper);

        public async Task Create(CreateCommentDTO creatingSettings, long userId)
        {
            if (_dbContext.OrdersItems.AsNoTracking().Any(i => i.ProductId == creatingSettings.ProductId && i.Order.UserId == userId))
                throw new NotFoundException();

            Comment comment = new Comment {
                UserId = userId,
                ProductId = creatingSettings.ProductId,
                Stars = creatingSettings.Stars,
                Text = creatingSettings.Text,
                CreatedDate = DateTime.Now
            };

            await _dbContext.Comments.AddAsync(comment);
            await _dbContext.SaveChangesAsync();
        }

        public async Task Delete(int productId, long userId)
        {
            _dbContext.Comments.Remove(await _dbContext.Comments.FirstAsync(c => c.UserId == userId && c.ProductId == productId));

            await _dbContext.SaveChangesAsync();
        }

        public async Task Edit(EditCommentDTO edittingSettings, long userId)
        {
            Comment comment = await _dbContext.Comments.FirstAsync(c => c.UserId == userId && c.ProductId == edittingSettings.ProductId);

            if (edittingSettings.Text != null)
                comment.Text = edittingSettings.Text;

            await _dbContext.SaveChangesAsync();
        }

        public async Task<ICollection<CommentDTO>> Get(GetCommentsDTO gettingSettings)
        {
            IQueryable<Comment> getCommentQuery = _dbContext.Comments.AsNoTracking();

            if (gettingSettings.UserId != null)
                getCommentQuery = getCommentQuery.Where(c => c.UserId == gettingSettings.UserId);

            if(gettingSettings.ProductId != null)
                getCommentQuery = getCommentQuery.Where(c => c.ProductId == gettingSettings.ProductId);

            if (gettingSettings.Stars != null)
                getCommentQuery = getCommentQuery.Where(c => c.Stars == gettingSettings.Stars);

            if (gettingSettings.FirstRangePoint != null && gettingSettings.EndRangePoint != null)
                getCommentQuery = getCommentQuery.Skip(gettingSettings.FirstRangePoint.Value)
                    .Take(gettingSettings.EndRangePoint.Value - gettingSettings.FirstRangePoint.Value);

            return (ICollection<CommentDTO>)await getCommentQuery.ProjectTo<GetCommentsDTO>(_mapper.ConfigurationProvider).ToListAsync();

        }
    }
}
