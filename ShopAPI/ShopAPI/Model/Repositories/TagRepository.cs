using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using ShopApi.Model.Entities.DTO.Tag;
using ShopApi.Model.Interfaces.Repository;
using ShopDb;
using ShopDb.Entities;

namespace ShopApi.Model.Repositories
{
    public class TagRepository : ITagRepository
    {
        private readonly IShopDbContext _dBContext;
        private readonly IMapper _mapper;

        public TagRepository(IShopDbContext dBContext, IMapper mapper)
            => (_dBContext, _mapper) = (dBContext, mapper);

        public async Task<int> Create(string name)
        {
            Tag creatingTag = new() { Name = name };
            await _dBContext.Tags.AddAsync(creatingTag);

            await _dBContext.SaveChangesAsync();
            return creatingTag.Id;
        }

        public async Task Delete(int tagId)
        {
            _dBContext.Tags.Remove(
                await _dBContext.Tags.AsNoTracking().FirstAsync(tag => tag.Id == tagId));

            await _dBContext.SaveChangesAsync();
        }

        public async Task<ICollection<TagDTO>> GetAll()
            => await _dBContext.Tags.AsNoTracking()
                .ProjectTo<TagDTO>(_mapper.ConfigurationProvider)
                .ToListAsync();
    }
}
