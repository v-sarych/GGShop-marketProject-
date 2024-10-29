using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using ShopAPICore.Entities.DTO.Tag;
using ShopAPICore.Exceptions;
using ShopAPICore.Interfaces.Repository;
using ShopDb;
using ShopDb.Entities;

namespace ShopAPICore.Repositories;

public class TagRepository : ITagRepository
{
    private readonly IShopDbContext _dBContext;
    private readonly IMapper _mapper;

    public TagRepository(IShopDbContext dBContext, IMapper mapper)
        => (_dBContext, _mapper) = (dBContext, mapper);

    public async Task<int> Create(string name)
    {
        if (await _dBContext.Tags.AsNoTracking().AnyAsync(x => x.Name == name))
            throw new AlreadyExistException();

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