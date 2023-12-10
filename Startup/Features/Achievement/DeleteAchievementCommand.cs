using EmploYee.Core.Repositories;
using EmploYee.Core.Specifications;
using Ftsoft.Application.Cqs.Mediatr;
using Ftsoft.Common.Result;
using Startup.Features.Employees.Models;

namespace Startup.Features.Achievement;

public class DeleteAchievementCommand : Command
{
    public long Id { get; set; }
}

public sealed class DeleteAchievementCommandHandler(IAchievementRepository achievementRepository) : CommandHandler<DeleteAchievementCommand>
{
    public override async Task<Result> Handle(DeleteAchievementCommand request, CancellationToken cancellationToken)
    {
        var achievement =
            await achievementRepository.SingleOrDefaultAsync(AchievementSpecification.GetById(request.Id).IsSatisfiedBy(),
                cancellationToken);
        await achievementRepository.RemoveAsync(achievement);
        await achievementRepository.UnitOfWork.SaveChangesAsync(cancellationToken);
        return Successful();
    }
}