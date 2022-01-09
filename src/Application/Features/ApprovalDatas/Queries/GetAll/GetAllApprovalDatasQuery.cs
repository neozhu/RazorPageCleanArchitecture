// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using CleanArchitecture.Razor.Application.Features.ApprovalDatas.DTOs;

namespace CleanArchitecture.Razor.Application.Features.ApprovalDatas.Queries.GetAll;

public class GetAllApprovalDatasQuery : IRequest<IEnumerable<ApprovalDataDto>>
{

}

public class GetAllApprovalDatasQueryHandler :
     IRequestHandler<GetAllApprovalDatasQuery, IEnumerable<ApprovalDataDto>>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;
    private readonly IStringLocalizer<GetAllApprovalDatasQueryHandler> _localizer;

    public GetAllApprovalDatasQueryHandler(
        IApplicationDbContext context,
        IMapper mapper,
        IStringLocalizer<GetAllApprovalDatasQueryHandler> localizer
        )
    {
        _context = context;
        _mapper = mapper;
        _localizer = localizer;
    }

    public Task<IEnumerable<ApprovalDataDto>> Handle(GetAllApprovalDatasQuery request, CancellationToken cancellationToken)
    {
        //TODO:Implementing GetAllApprovalDatasQueryHandler method 
        throw new NotImplementedException();
    }
}

