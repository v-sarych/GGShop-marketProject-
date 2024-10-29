using AutoMapper;
using ShopAPICore.Mapping.Profiles.DTO;

namespace ShopAPICore.Mapping;

public static class AutoMapperConfiguration
{
    public static IMapperConfigurationExpression GetConfiguration(IMapperConfigurationExpression cfg)
    {
        cfg.AddProfile<UserProfile>();
        cfg.AddProfile<OrderProfile>();
        cfg.AddProfile<ShoppingCartProfile>();
        cfg.AddProfile<CommentProfile>();
        cfg.AddProfile<ProductAvailabilityProfile>();
        cfg.AddProfile<ProductProfile>();
        cfg.AddProfile<TagProfile>();

        return cfg;
    }
}