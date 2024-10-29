namespace Identity.Core.Model;

public class JwtConfiguration
{
    public readonly string Issuer = "IdentityServer";
    public readonly int LifeTimeInMinutes = 20;

    public readonly string SymetricKey = "yu+6o54fgyu65i,48kdft674nd4u+rt618h4fndZBFR";

    public JwtConfiguration() { }
}