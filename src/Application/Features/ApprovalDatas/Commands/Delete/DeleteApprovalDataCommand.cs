// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace CleanArchitecture.Razor.Application.Features.ApprovalDatas.Commands.Delete;

public class DeleteApprovalDataCommand : IRequest<Result>
{

}
public class DeleteCheckedApprovalDatasCommand : IRequest<Result>
{

}

public class DeleteApprovalDataCommandHandler :
             IRequestHandler<DeleteApprovalDataCommand, Result>,
             IRequestHandler<DeleteCheckedApprovalDatasCommand, Result>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;
    private readonly IStringLocalizer<DeleteApprovalDataCommandHandler> _localizer;
    public DeleteApprovalDataCommandHandler(
        IApplicationDbContext context,
        IStringLocalizer<DeleteApprovalDataCommandHandler> localizer,
         IMapper mapper
        )
    {
        _context = context;
        _localizer = localizer;
        _mapper = mapper;
    }
    public Task<Result> Handle(DeleteApprovalDataCommand request, CancellationToken cancellationToken)
    {
        //TODO:Implementing DeleteApprovalDataCommandHandler method 
        throw new System.NotImplementedException();
    }

    public Task<Result> Handle(DeleteCheckedApprovalDatasCommand request, CancellationToken cancellationToken)
    {
        //TODO:Implementing DeleteCheckedApprovalDatasCommandHandler method 
        throw new System.NotImplementedException();
    }
}
