using EmploYee.Core.Repositories;
using EmploYee.Core.Specifications;
using Ftsoft.Application.Cqs.Mediatr;
using Ftsoft.Common.Result;

namespace Startup.Features.Achievement;

public class UpdateAchievementCommand : Command
{
    public long Id { get; set; }
    public string Title { get; set; } 
    public string Image { get; set; } 
    public double CurrentValue { get; set; } 
}


public sealed class UpdateAchievementCommandHandler(IAchievementRepository achievementRepository) : CommandHandler<UpdateAchievementCommand>
{
    public override async Task<Result> Handle(UpdateAchievementCommand request, CancellationToken cancellationToken)
    {
        var achievement =
            await achievementRepository.SingleOrDefaultAsync(AchievementSpecification.GetById(request.Id).IsSatisfiedBy(),
                cancellationToken);
        achievement.Update(request.Title, request.Image, request.CurrentValue);
        await achievementRepository.UnitOfWork.SaveChangesAsync(cancellationToken);
        return Successful();
    }
}