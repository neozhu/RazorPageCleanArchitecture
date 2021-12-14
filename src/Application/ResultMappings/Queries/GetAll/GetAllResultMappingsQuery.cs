// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using CleanArchitecture.Razor.Application.ResultMappings.DTOs;

namespace CleanArchitecture.Razor.Application.ResultMappings.Queries.GetAll;

    public class GetAllResultMappingsQuery : IRequest<IEnumerable<ResultMappingDto>>
    {
       
    }
    
    public class GetAllResultMappingsQueryHandler :
         IRequestHandler<GetAllResultMappingsQuery, IEnumerable<ResultMappingDto>>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly IStringLocalizer<GetAllResultMappingsQueryHandler> _localizer;

        public GetAllResultMappingsQueryHandler(
            IApplicationDbContext context,
            IMapper mapper,
            IStringLocalizer<GetAllResultMappingsQueryHandler> localizer
            )
        {
            _context = context;
            _mapper = mapper;
            _localizer = localizer;
        }

        public async Task<IEnumerable<ResultMappingDto>> Handle(GetAllResultMappingsQuery request, CancellationToken cancellationToken)
        {
  
            var data = await _context.ResultMappings
                         .ProjectTo<ResultMappingDto>(_mapper.ConfigurationProvider)
                         .ToListAsync(cancellationToken);
            return data;
        }
    }


