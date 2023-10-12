using ShopApiCore.Entities.DTO.Tag;

namespace ShopApiCore.Interfaces.Repository
{
    public interface ITagRepository
    {
        Task<int> Create(string name);
        Task Delete(int tagId);
        Task<ICollection<TagDTO>> GetAll();
    }
}
