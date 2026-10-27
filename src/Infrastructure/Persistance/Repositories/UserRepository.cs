using Microsoft.EntityFrameworkCore;
using WorkOMat.Application.Common.Repositories;
using WorkOMat.Domain.User;

namespace WorkOMat.Infrastructure.Persistance.Repositories;

public class UserRepository : Repository<User, UserId>, IUserRepository
{
    public UserRepository(AppDbContext context) : base(context)
    {
    }
}