using ShopAPICore.Entities.DTO.Tag;

namespace ShopAPICore.Interfaces.Repository;

public interface ITagRepository
{
    Task<int> Create(string name);
    Task Delete(int tagId);
    Task<ICollection<TagDTO>> GetAll();
}