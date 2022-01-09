// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using CleanArchitecture.Razor.Application.Features.ApprovalDatas.DTOs;

namespace CleanArchitecture.Razor.Application.Features.ApprovalDatas.Commands.Update;

public class UpdateApprovalDataCommand : ApprovalDataDto, IRequest<Result>
{

}

public class UpdateApprovalDataCommandHandler : IRequestHandler<UpdateApprovalDataCommand, Result>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;
    private readonly IStringLocalizer<UpdateApprovalDataCommandHandler> _localizer;
    public UpdateApprovalDataCommandHandler(
        IApplicationDbContext context,
        IStringLocalizer<UpdateApprovalDataCommandHandler> localizer,
         IMapper mapper
        )
    {
        _context = context;
        _localizer = localizer;
        _mapper = mapper;
    }
    public Task<Result> Handle(UpdateApprovalDataCommand request, CancellationToken cancellationToken)
    {
        //TODO:Implementing UpdateApprovalDataCommandHandler method 
        throw new System.NotImplementedException();
    }
}
