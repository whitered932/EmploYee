using EmploYee.Core.Models;
using EmploYee.Core.Repositories;
using Ftsoft.Application.Cqs.Mediatr;
using Ftsoft.Common.Result;
using Microsoft.AspNetCore.Mvc.ActionConstraints;
using Startup.Features.Employees.Models;

namespace Startup.Features.Achievement;

public class CreateAchievementCommand : Command
{
    public string Title { get; private set; }
    public string Image { get; private set; }
    public double CurrentValue { get; private set; }
}

public sealed class CreateAchievementCommandHandler
    (IAchievementRepository achievementRepository) : CommandHandler<CreateAchievementCommand>
{
    
    public override async Task<Result> Handle(CreateAchievementCommand request, CancellationToken cancellationToken)
    {
        var achievement = new EmploYee.Core.Models.Achievement(request.Title, request.Image, request.CurrentValue);
        await achievementRepository.AddAsync(achievement, cancellationToken);
        await achievementRepository.UnitOfWork.SaveChangesAsync(cancellationToken);
        return Successful();
    }
}