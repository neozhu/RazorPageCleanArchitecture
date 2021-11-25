// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using CleanArchitecture.Razor.Application.MappingRules.DTOs;

namespace CleanArchitecture.Razor.Application.MappingRules.Commands.Delete;

    public class DeleteMappingRuleCommand: IRequest<Result>
    {
      public int Id {  get; set; }
    }
    public class DeleteCheckedMappingRulesCommand : IRequest<Result>
    {
      public int[] Id {  get; set; }
    }

    public class DeleteMappingRuleCommandHandler : 
                 IRequestHandler<DeleteMappingRuleCommand, Result>,
                 IRequestHandler<DeleteCheckedMappingRulesCommand, Result>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly IStringLocalizer<DeleteMappingRuleCommandHandler> _localizer;
        public DeleteMappingRuleCommandHandler(
            IApplicationDbContext context,
            IStringLocalizer<DeleteMappingRuleCommandHandler> localizer,
             IMapper mapper
            )
        {
            _context = context;
            _localizer = localizer;
            _mapper = mapper;
        }
        public async Task<Result> Handle(DeleteMappingRuleCommand request, CancellationToken cancellationToken)
        {

           var item = await _context.MappingRules.FindAsync(new object[] { request.Id }, cancellationToken);
            _context.MappingRules.Remove(item);
            await _context.SaveChangesAsync(cancellationToken);
            return Result.Success();
        }

        public async Task<Result> Handle(DeleteCheckedMappingRulesCommand request, CancellationToken cancellationToken)
        {

           var items = await _context.MappingRules.Where(x => request.Id.Contains(x.Id)).ToListAsync(cancellationToken);
            foreach (var item in items)
            {
                _context.MappingRules.Remove(item);
            }
            await _context.SaveChangesAsync(cancellationToken);
            return Result.Success();
        }
    }

