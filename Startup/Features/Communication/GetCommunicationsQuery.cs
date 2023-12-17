using EmploYee.Core.Repositories;
using Ftsoft.Application.Cqs.Mediatr;
using Ftsoft.Common.Result;
using Startup.Features.Communication.Models;

namespace Startup.Features.Communication;

public class GetCommunicationsQuery : Query<IReadOnlyList<CommunicationDto>>
{
}

public sealed class GetCommunicationsQueryHandler
    (IAdministratorRepository administratorRepository) : QueryHandler<GetCommunicationsQuery,
        IReadOnlyList<CommunicationDto>>
{
    public override async Task<Result<IReadOnlyList<CommunicationDto>>> Handle(GetCommunicationsQuery request,
        CancellationToken cancellationToken)
    {
        var administrators = await administratorRepository.ListAsync(cancellationToken);
        var communicationDtos = administrators
            .Select(x => new CommunicationDto()
            {
                Email = x.Email,
                Name = x.GetFullName(),
                Phone = x.PhoneNumber,
                Role = x.Role.ToString(),
                ImgSrc =
                    "https://avatars.dzeninfra.ru/get-zen_doc/1926321/pub_5f9fe0ef3910530e0d012a5c_5f9fe13b49505f68110f98f1/scale_1200"
            })
            .ToList();
        return Successful(communicationDtos);
    }
}