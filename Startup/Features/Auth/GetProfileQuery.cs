using Ftsoft.Application.Cqs.Mediatr;
using Ftsoft.Common.Result;
using Startup.Providers;

namespace Startup.Features.Auth;

public class GetProfileQuery : Query<AccountInfoDto>
{
}

public sealed class GetAccountInfoQueryHandler
    (ISecurityProvider securityProvider) : QueryHandler<GetProfileQuery, AccountInfoDto>
{
    public override async Task<Result<AccountInfoDto>> Handle(GetProfileQuery request,
        CancellationToken cancellationToken)
    {
        var user = securityProvider.SecurityProfile.User;
        var accountDto = new AccountInfoDto()
            { Email = user.Email, Id = user.Id, Name = user.Name, Role = user.Role.ToString() };
        return Successful(accountDto);
    }
}

public class AccountInfoDto
{
    public long Id { get; set; }
    public string Email { get; set; }
    public string Name { get; set; }
    public string Role { get; set; }
}