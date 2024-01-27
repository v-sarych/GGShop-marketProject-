using AutoMapper;
using IdentityServer.Model.Entities.DTO;
using IdentityServer.Model.Entities.Identity;
using IdentityServer.Model.Helpers;
using IdentityServer.Model.interfaces.Repositories;
using Microsoft.EntityFrameworkCore;
using ShopDb;
using ShopDb.Entities;
using System.Runtime.Intrinsics.Arm;
using System.Security.Cryptography;
using System.Text;

namespace IdentityServer.Model
{
    public class SessionRepository : ISessionRepository
    {
        private readonly IShopDbContext _context;
        private readonly IMapper _mapper;

        public SessionRepository(IShopDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<Session> Create(SessionCreateInfo info)
        {
            Session session = new Session()
            {
                UserId = info.UserId,
                Id = info.Id,
                Role = info.Role,
                RefreshTokenHash = Hasher.GetHash(info.RefreshToken)
            };

            await _context.Sessions.AddAsync(session);
            await _context.SaveChangesAsync();

            return session;
        }

        public async Task Delete(Guid sessionId)
        {
            _context.Sessions.Remove(await _context.Sessions.FirstAsync(s => s.Id == sessionId));
            await _context.SaveChangesAsync();
        }

        public async Task<ICollection<SessionDTO>> GetAll(long userId)
            => _mapper.Map<List<SessionDTO>>(await _context.Sessions.AsNoTracking().Where(session => session.UserId == userId).ToArrayAsync());

        public async Task<bool> TryFind(Guid id)
            =>  await _context.Sessions.FirstOrDefaultAsync(session => session.Id == id) != null;

        public async Task<bool> TryFind(Guid id, string refreshToken)
            => await _context.Sessions.FirstOrDefaultAsync(session => session.Id == id && session.RefreshTokenHash == Hasher.GetHash(refreshToken)) != null;

        public async Task<Session> FindSessionOrDefault(Guid id)
        {
            return await _context.Sessions.AsNoTracking().FirstOrDefaultAsync(session => session.Id == id);
        }

        public async Task<Session> Update(Guid id, string newRefreshToken)
        {
            Session session = await _context.Sessions.FirstAsync(session => session.Id ==id);

            session.RefreshTokenHash = Hasher.GetHash(newRefreshToken);

            await _context.SaveChangesAsync();

            return session;
        }
    }
}
