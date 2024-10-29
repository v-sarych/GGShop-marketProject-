namespace Integrations.Cdek.Entities.OAuth;

public class AccessObject
{
    public string Access_token { get; set; }
    public string Token_type { get; set; }
    public int Expires_in { get; set; }
    public string Scope { get; set; }
    public string Jti { get; set;}
}