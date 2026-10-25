using WorkOMat.Domain.Shared;

namespace WorkOMat.Domain.User;

public class User : AggregateRoot<UserId>
{
    public string DisplayName { get; private set; }
    public IdentityUserId IdentityId { get; private set; }
    
    public User(UserId id, string displayName, IdentityUserId identityId) : base(id)
    {
        DisplayName = displayName;
        IdentityId = identityId;
    }
}