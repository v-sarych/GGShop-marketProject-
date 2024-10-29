using Identity.Core.Model.Entities.Identity;

namespace Identity.Core.Model.interfaces;

public interface IIdentityUserMapper<T>
{
    T FromIdentity(UserIdentity userIdentity);
    UserIdentity ToIdentity(T user);
}