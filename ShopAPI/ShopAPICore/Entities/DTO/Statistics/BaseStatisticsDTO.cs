using ShopApiCore.Entities.DTO.ProductSearch;

namespace ShopApiCore.Entities.DTO.Statistics
{
    public class BaseStatisticsDTO
    {
        public int OrderCount {  get; set; }
        public float AverageCheck { get; set; }
        public float SumOfOrders { get; set; }
        public ICollection<SimpleProductDTO> MostPopularProducts { get; set; }
    }
}
