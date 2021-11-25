// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using CleanArchitecture.Razor.Application.FieldMappingValues.DTOs;

namespace CleanArchitecture.Razor.Application.FieldMappingValues.Commands.Create;

    public class CreateFieldMappingValueCommand: FieldMappingValueDto,IRequest<Result<int>>, IMapFrom<FieldMappingValue>
    {
       
    }
    
    public class CreateFieldMappingValueCommandHandler : IRequestHandler<CreateFieldMappingValueCommand, Result<int>>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly IStringLocalizer<CreateFieldMappingValueCommand> _localizer;
        public CreateFieldMappingValueCommandHandler(
            IApplicationDbContext context,
            IStringLocalizer<CreateFieldMappingValueCommand> localizer,
            IMapper mapper
            )
        {
            _context = context;
            _localizer = localizer;
            _mapper = mapper;
        }
        public async Task<Result<int>> Handle(CreateFieldMappingValueCommand request, CancellationToken cancellationToken)
        {

           var item = _mapper.Map<FieldMappingValue>(request);
           _context.FieldMappingValues.Add(item);
           await _context.SaveChangesAsync(cancellationToken);
           return  Result<int>.Success(item.Id);
        }
    }

