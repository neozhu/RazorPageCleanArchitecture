// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using CleanArchitecture.Razor.Application.FieldValueMappings.DTOs;

namespace CleanArchitecture.Razor.Application.FieldValueMappings.Queries.GetAll;

    public class GetAllFieldValueMappingsQuery : IRequest<IEnumerable<FieldValueMappingDto>>
    {
       
    }
    
    public class GetAllFieldValueMappingsQueryHandler :
         IRequestHandler<GetAllFieldValueMappingsQuery, IEnumerable<FieldValueMappingDto>>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly IStringLocalizer<GetAllFieldValueMappingsQueryHandler> _localizer;

        public GetAllFieldValueMappingsQueryHandler(
            IApplicationDbContext context,
            IMapper mapper,
            IStringLocalizer<GetAllFieldValueMappingsQueryHandler> localizer
            )
        {
            _context = context;
            _mapper = mapper;
            _localizer = localizer;
        }

        public async Task<IEnumerable<FieldValueMappingDto>> Handle(GetAllFieldValueMappingsQuery request, CancellationToken cancellationToken)
        {
            //TODO:Implementing GetAllFieldValueMappingsQueryHandler method 
            var data = await _context.FieldValueMappings
                         .ProjectTo<FieldValueMappingDto>(_mapper.ConfigurationProvider)
                         .ToListAsync(cancellationToken);
            return data;
        }
    }


