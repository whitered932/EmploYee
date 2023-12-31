﻿using EmploYee.Core.Repositories;
using EmploYee.Core.Specifications;
using Ftsoft.Application.Cqs.Mediatr;
using Ftsoft.Common.Result;
using Startup.Features.Errors;

namespace Startup.Features.Stage;

public class DeleteStageCommand : Command
{
    public long Id { get; set; }
}

public sealed class DeleteStageCommandHandler(IStageRepository stageRepository) : CommandHandler<DeleteStageCommand>
{
    public override async Task<Result> Handle(DeleteStageCommand request, CancellationToken cancellationToken)
    {
        var stage = await stageRepository.SingleOrDefaultAsync(StageSpecification.GetById(request.Id).IsSatisfiedBy(),
            cancellationToken);
        if (stage is null)
        {
            return Error(NotFoundError.Instance);
        }
        await stageRepository.RemoveAsync(stage);
        await stageRepository.UnitOfWork.SaveChangesAsync(cancellationToken);
        return Successful();
    }
}