using AutoMapper;
using ShopApiCore.Mapping.Profiles.DTO;

namespace ShopApiCore.Mapping
{
    public static class AutoMapperConfiguration
    {
        public static IMapperConfigurationExpression GetConfiguration(IMapperConfigurationExpression cfg)
        {
            cfg.AddProfile<CommentProfile>();
            cfg.AddProfile<OrderProfile>();
            cfg.AddProfile<ProductAvailabilityProfile>();
            cfg.AddProfile<ProductProfile>();
            cfg.AddProfile<ShoppingCartProfile>();
            cfg.AddProfile<TagProfile>();
            cfg.AddProfile<UserProfile>();

            return cfg;
        }
    }
}
