using IdentityServer.Model.Entities.Identity;

namespace IdentityServer.Model.interfaces
{
    public interface IIdentityUserMapper<T>
    {
        T FromIdentity(UserIdentity userIdentity);
        UserIdentity ToIdentity(T user);
    }
}
