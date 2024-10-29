using AutoMapper;
using Identity.Core.Model.Entities.DTO;
using Identity.Core.Model.Entities.Identity;
using Identity.Core.Model.interfaces.Repositories;
using Microsoft.EntityFrameworkCore;
using ShopDb;
using ShopDb.Entities;

namespace Identity.Core.Model;

public class UserRepository : IUserRepository
{
    private readonly IMapper _mapper;
    private readonly IShopDbContext _context;

    public UserRepository(IShopDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<bool> TryFindUser(string phoneNumber)
        => await _context.Users.AsNoTracking().FirstOrDefaultAsync(user => user.PhoneNumber == phoneNumber) != null;

    public async Task<UserIdentity> FindUserOrDefault(string phoneNumber)
    {
        var user = await _context.Users.AsNoTracking().FirstOrDefaultAsync(user => user.PhoneNumber == phoneNumber);
        if (user == null)
            return null;

        return _mapper.Map<UserIdentity>(user);
    }

    public async Task<UserIdentity> Create(RegisterUserDTO registerUser)
    {
        var user = new User()
        {
            PhoneNumber = registerUser.PhoneNumber,
            Password = registerUser.Password
        };
        await _context.Users.AddAsync(user);

        await _context.SaveChangesAsync();

        return _mapper.Map<UserIdentity>(user);
    }

    public async Task Delete(long id)
    {
        _context.Users.Remove(_context.Users.First(user => user.Id == id));
        await _context.SaveChangesAsync();
    }

    public async Task UpdatePassword(long id, string newPassword)
    {
        (await _context.Users.FirstAsync(user => user.Id == id)).Password = newPassword;
        await _context.SaveChangesAsync();
    }

    public async Task UpdatePhoneNumber(long id, string newPhoneNumber)
    {
        (await _context.Users.FirstAsync(user => user.Id == id)).PhoneNumber = newPhoneNumber;
        await _context.SaveChangesAsync();
    }
}