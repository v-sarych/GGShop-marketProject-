using ShopApi.Model.Entities.DTO.Tag;

namespace ShopApi.Model.Entities.DTO.ProductControll
{
    public class UpdateProductDTO
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public bool? CanBeFound { get; set; }

        public ICollection<int>? TagsIds { get; set; }
            //сюда передаются все теги продукта которые должны у него стоять
    }
}
