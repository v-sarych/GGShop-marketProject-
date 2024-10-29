using Identity.Core.Model.Entities.Identity;
using Identity.Core.Model.Exceptions;
using Identity.Core.Model.interfaces;
using Identity.Core.Model.interfaces.Repositories;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;

namespace Identity.Core.Model;

public class AuthenticationService : IAuthenticationService
{
    private readonly IJwtCreator _jwtCreator;
    private readonly ISessionRepository _sessionRepository;

    public AuthenticationService(IJwtCreator jwtCreator, ISessionRepository sessionRepository)
    {
        _jwtCreator = jwtCreator;
        _sessionRepository = sessionRepository;
    }

    public async Task<string> Login(UserIdentity user)
    {
        var sessionId = Guid.NewGuid();

        var newRefreshToken = _createRandomToken();
        var session = await _sessionRepository.Create(new SessionCreateInfo()
        {
            UserId = user.Id,
            Id = sessionId,
            Role = user.Role,
            RefreshToken = newRefreshToken
        });

        var jwt = _jwtCreator.Create(new TokenPayload()
        {
            Id = session.Id,
            RefreshToken = newRefreshToken,
            UsertId = session.UserId,
            Role = session.Role
        });

        return new JwtSecurityTokenHandler().WriteToken(jwt);
    }

    public async Task<string> Update(TokenPayload payload)
    {
        if (!await _sessionRepository.TryFind(payload.Id, payload.RefreshToken))
            throw new NotFoundException();

        var newRefreshToken = _createRandomToken();
        var session = await _sessionRepository.Update(payload.Id, newRefreshToken);

        payload.RefreshToken = newRefreshToken;

        var jwt = _jwtCreator.Create(payload);
        return new JwtSecurityTokenHandler().WriteToken(jwt);
    }

    public async Task<ICollection<Claim>> GetJwtClaims(string jwtString)
        => new JwtSecurityTokenHandler().ReadJwtToken(jwtString).Claims.ToList();

    public async Task DeleteSession(Guid id)
        => await _sessionRepository.Delete(id);

    private string _createRandomToken()
        => Convert.ToBase64String(RandomNumberGenerator.GetBytes(32));
}