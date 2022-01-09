// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using CleanArchitecture.Razor.Application.Features.ApprovalDatas.DTOs;

namespace CleanArchitecture.Razor.Application.Features.ApprovalDatas.Commands.AddEdit;

public class AddEditApprovalDataCommand : ApprovalDataDto, IRequest<Result>
{

}

public class AddEditApprovalDataCommandHandler : IRequestHandler<AddEditApprovalDataCommand, Result>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;
    private readonly IStringLocalizer<AddEditApprovalDataCommandHandler> _localizer;
    public AddEditApprovalDataCommandHandler(
        IApplicationDbContext context,
        IStringLocalizer<AddEditApprovalDataCommandHandler> localizer,
        IMapper mapper
        )
    {
        _context = context;
        _localizer = localizer;
        _mapper = mapper;
    }
    public Task<Result> Handle(AddEditApprovalDataCommand request, CancellationToken cancellationToken)
    {
        //TODO:Implementing AddEditApprovalDataCommandHandler method 
        throw new System.NotImplementedException();
    }
}
