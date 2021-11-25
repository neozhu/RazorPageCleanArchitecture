// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using CleanArchitecture.Razor.Application.FieldMappingValues.DTOs;

namespace CleanArchitecture.Razor.Application.FieldMappingValues.Queries.GetAll;

    public class GetAllFieldMappingValuesQuery : IRequest<IEnumerable<FieldMappingValueDto>>
    {
       
    }
    
    public class GetAllFieldMappingValuesQueryHandler :
         IRequestHandler<GetAllFieldMappingValuesQuery, IEnumerable<FieldMappingValueDto>>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly IStringLocalizer<GetAllFieldMappingValuesQueryHandler> _localizer;

        public GetAllFieldMappingValuesQueryHandler(
            IApplicationDbContext context,
            IMapper mapper,
            IStringLocalizer<GetAllFieldMappingValuesQueryHandler> localizer
            )
        {
            _context = context;
            _mapper = mapper;
            _localizer = localizer;
        }

        public async Task<IEnumerable<FieldMappingValueDto>> Handle(GetAllFieldMappingValuesQuery request, CancellationToken cancellationToken)
        {
             var data = await _context.FieldMappingValues
                         .ProjectTo<FieldMappingValueDto>(_mapper.ConfigurationProvider)
                         .ToListAsync(cancellationToken);
            return data;
        }
    }


