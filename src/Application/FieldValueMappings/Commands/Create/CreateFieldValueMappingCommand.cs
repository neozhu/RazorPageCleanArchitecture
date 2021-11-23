// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using CleanArchitecture.Razor.Application.FieldValueMappings.DTOs;

namespace CleanArchitecture.Razor.Application.FieldValueMappings.Commands.Create;

    public class CreateFieldValueMappingCommand: FieldValueMappingDto,IRequest<Result<int>>, IMapFrom<FieldValueMapping>
    {
       
    }
    
    public class CreateFieldValueMappingCommandHandler : IRequestHandler<CreateFieldValueMappingCommand, Result<int>>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly IStringLocalizer<CreateFieldValueMappingCommand> _localizer;
        public CreateFieldValueMappingCommandHandler(
            IApplicationDbContext context,
            IStringLocalizer<CreateFieldValueMappingCommand> localizer,
            IMapper mapper
            )
        {
            _context = context;
            _localizer = localizer;
            _mapper = mapper;
        }
        public async Task<Result<int>> Handle(CreateFieldValueMappingCommand request, CancellationToken cancellationToken)
        {
           
           var item = _mapper.Map<FieldValueMapping>(request);
           _context.FieldValueMappings.Add(item);
           await _context.SaveChangesAsync(cancellationToken);
           return  Result<int>.Success(item.Id);
        }
    }

