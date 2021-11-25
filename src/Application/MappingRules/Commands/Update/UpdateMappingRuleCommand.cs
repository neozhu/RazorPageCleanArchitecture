// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using CleanArchitecture.Razor.Application.MappingRules.DTOs;

namespace CleanArchitecture.Razor.Application.MappingRules.Commands.Update;

    public class UpdateMappingRuleCommand: MappingRuleDto,IRequest<Result>, IMapFrom<MappingRule>
    {
        
    }

    public class UpdateMappingRuleCommandHandler : IRequestHandler<UpdateMappingRuleCommand, Result>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly IStringLocalizer<UpdateMappingRuleCommandHandler> _localizer;
        public UpdateMappingRuleCommandHandler(
            IApplicationDbContext context,
            IStringLocalizer<UpdateMappingRuleCommandHandler> localizer,
             IMapper mapper
            )
        {
            _context = context;
            _localizer = localizer;
            _mapper = mapper;
        }
        public async Task<Result> Handle(UpdateMappingRuleCommand request, CancellationToken cancellationToken)
        {
           var item =await _context.MappingRules.FindAsync( new object[] { request.Id }, cancellationToken);
           if (item != null)
           {
                item = _mapper.Map(request, item);
                await _context.SaveChangesAsync(cancellationToken);
           }
           return Result.Success();
        }
    }

