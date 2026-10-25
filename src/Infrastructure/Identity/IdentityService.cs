using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using WorkOMat.Application.Common.Interfaces;
using WorkOMat.Application.Common.Result;
using WorkOMat.Domain.User;
using WorkOMat.Infrastructure.Persistance;

namespace WorkOMat.Infrastructure.Identity;

public class IdentityService : IIdentityService
{
    private readonly AppDbContext _dbContext;
    private readonly UserManager<ApplicationUser> _userManager;

    public IdentityService(AppDbContext dbContext, IUnitOfWork unitOfWork, UserManager<ApplicationUser> userManager)
    {
        _dbContext = dbContext;
        _userManager = userManager;
    }

    public async Task<Result<User>> RegisterAsync(string displayName, string email, string password)
    {
        var user = new ApplicationUser
        {
            Email = email,
            UserName = email
        };
        var result = await _userManager.CreateAsync(user, password);
        if (!result.Succeeded)
        {
            return Result<User>.Failure([new Error(ErrorKind.Conflict, "User.AlreadyExists")]);
        }

        var domainUser = new User(
            id: new UserId(Guid.NewGuid()),
            displayName: displayName,
            identityId: IdentityUserId.Parse(user.Id)
        );
        _dbContext.Users.Add(domainUser);
        return Result<User>.Success(domainUser);
    }

    public async Task<Result<User>> LoginAsync(string email, string password)
    {
        var user = await _userManager.FindByEmailAsync(email);
        if (user == null)
        {
            return Result<User>.Failure([new Error(ErrorKind.NotFound, "User.NotFound")]);
        }

        var result = await _userManager.CheckPasswordAsync(user, password);
        if (!result)
        {
            return Result<User>.Failure([new Error(ErrorKind.NotFound, "User.NotFound")]);
        }

        var domainUser = await _dbContext.Users.FirstOrDefaultAsync(u => u.IdentityId == IdentityUserId.Parse(user.Id));
        if (domainUser == null)
        {
            await _userManager.DeleteAsync(user);
            return Result<User>.Failure([new Error(ErrorKind.NotFound, "User.NotFound")]);
        }

        return Result<User>.Success(domainUser);
    }
}