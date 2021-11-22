// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using CleanArchitecture.Razor.Application.ObjectFields.DTOs;

namespace CleanArchitecture.Razor.Application.ObjectFields.Queries.GetAll;

    public class GetAllObjectFieldsQuery : IRequest<IEnumerable<ObjectFieldDto>>
    {
       
    }
    
    public class GetAllObjectFieldsQueryHandler :
         IRequestHandler<GetAllObjectFieldsQuery, IEnumerable<ObjectFieldDto>>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly IStringLocalizer<GetAllObjectFieldsQueryHandler> _localizer;

        public GetAllObjectFieldsQueryHandler(
            IApplicationDbContext context,
            IMapper mapper,
            IStringLocalizer<GetAllObjectFieldsQueryHandler> localizer
            )
        {
            _context = context;
            _mapper = mapper;
            _localizer = localizer;
        }

        public async Task<IEnumerable<ObjectFieldDto>> Handle(GetAllObjectFieldsQuery request, CancellationToken cancellationToken)
        {

            var data = await _context.ObjectFields
                         .ProjectTo<ObjectFieldDto>(_mapper.ConfigurationProvider)
                         .ToListAsync(cancellationToken);
            return data;
        }
    }


