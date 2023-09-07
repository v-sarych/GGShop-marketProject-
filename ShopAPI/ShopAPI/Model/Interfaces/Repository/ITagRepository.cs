using ShopApi.Model.Entities.DTO.Tag;

namespace ShopApi.Model.Interfaces.Repository
{
    public interface ITagRepository
    {
        Task<int> Create(string name);
        Task Delete(int tagId);
        Task<ICollection<TagDTO>> GetAll();
    }
}
