// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using CleanArchitecture.Razor.Application.MappingRules.DTOs;

namespace CleanArchitecture.Razor.Application.MappingRules.Commands.Create;

    public class CreateMappingRuleCommand: MappingRuleDto,IRequest<Result<int>>, IMapFrom<MappingRule>
{
       
    }
    
    public class CreateMappingRuleCommandHandler : IRequestHandler<CreateMappingRuleCommand, Result<int>>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly IStringLocalizer<CreateMappingRuleCommand> _localizer;
        public CreateMappingRuleCommandHandler(
            IApplicationDbContext context,
            IStringLocalizer<CreateMappingRuleCommand> localizer,
            IMapper mapper
            )
        {
            _context = context;
            _localizer = localizer;
            _mapper = mapper;
        }
        public async Task<Result<int>> Handle(CreateMappingRuleCommand request, CancellationToken cancellationToken)
        {
           var item = _mapper.Map<MappingRule>(request);
           _context.MappingRules.Add(item);
           await _context.SaveChangesAsync(cancellationToken);
           return  Result<int>.Success(item.Id);
        }
    }

