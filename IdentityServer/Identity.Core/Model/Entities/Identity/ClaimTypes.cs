namespace Identity.Core.Model.Entities.Identity;

public static class ClaimTypes
{
    public const string UserId = "UserId";
    public const string SessionId = "SessionId";
    public const string RefreshToken = "RefreshToken";
    public const string Role = System.Security.Claims.ClaimTypes.Role;
}