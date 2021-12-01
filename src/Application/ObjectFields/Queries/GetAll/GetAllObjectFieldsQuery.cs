// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using CleanArchitecture.Razor.Application.ObjectFields.DTOs;

namespace CleanArchitecture.Razor.Application.ObjectFields.Queries.GetAll;

public class GetAllObjectFieldsQuery : IRequest<IEnumerable<ObjectFieldDto>>
{

}

public class GetAllObjectFieldsWithKeyQuery : IRequest<IEnumerable<ObjectFieldDto>>
{
    public string Key { get; set; } = string.Empty;
}

public class GetAllObjectFieldsQueryHandler :
         IRequestHandler<GetAllObjectFieldsWithKeyQuery, IEnumerable<ObjectFieldDto>>,
         IRequestHandler<GetAllObjectFieldsQuery, IEnumerable<ObjectFieldDto>>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;
    private readonly ICurrentUserService _userService;
    private readonly IStringLocalizer<GetAllObjectFieldsQueryHandler> _localizer;

    public GetAllObjectFieldsQueryHandler(
        IApplicationDbContext context,
        IMapper mapper,
        ICurrentUserService userService,
        IStringLocalizer<GetAllObjectFieldsQueryHandler> localizer
        )
    {
        _context = context;
        _mapper = mapper;
        _userService = userService;
        _localizer = localizer;
    }

    public async Task<IEnumerable<ObjectFieldDto>> Handle(GetAllObjectFieldsQuery request, CancellationToken cancellationToken)
    {
        var currentProjectId = _userService.ProjectId();

        var data = await _context.ObjectFields.Where(x=>x.MigrationProjectId==currentProjectId)
                     .ProjectTo<ObjectFieldDto>(_mapper.ConfigurationProvider)
                     .ToListAsync(cancellationToken);
        return data;
    }
    public async Task<IEnumerable<ObjectFieldDto>> Handle(GetAllObjectFieldsWithKeyQuery request, CancellationToken cancellationToken)
    {

        var data = await _context.ObjectFields.Where(x => x.Name.Contains(request.Key) || x.Description.Contains(request.Key))
                     .OrderBy(x => x.Name)
                     .ProjectTo<ObjectFieldDto>(_mapper.ConfigurationProvider)
                     .ToListAsync(cancellationToken);
        return data;
    }
}


