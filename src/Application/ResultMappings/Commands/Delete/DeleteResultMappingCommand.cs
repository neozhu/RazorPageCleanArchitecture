// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using CleanArchitecture.Razor.Application.ResultMappings.DTOs;

namespace CleanArchitecture.Razor.Application.ResultMappings.Commands.Delete;

    public class DeleteResultMappingCommand: IRequest<Result>
    {
      public int Id {  get; set; }
    }
    public class DeleteCheckedResultMappingsCommand : IRequest<Result>
    {
      public int[] Id {  get; set; }
    }

    public class DeleteResultMappingCommandHandler : 
                 IRequestHandler<DeleteResultMappingCommand, Result>,
                 IRequestHandler<DeleteCheckedResultMappingsCommand, Result>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly IStringLocalizer<DeleteResultMappingCommandHandler> _localizer;
        public DeleteResultMappingCommandHandler(
            IApplicationDbContext context,
            IStringLocalizer<DeleteResultMappingCommandHandler> localizer,
             IMapper mapper
            )
        {
            _context = context;
            _localizer = localizer;
            _mapper = mapper;
        }
        public async Task<Result> Handle(DeleteResultMappingCommand request, CancellationToken cancellationToken)
        {

           var item = await _context.ResultMappings.FindAsync(new object[] { request.Id }, cancellationToken);
            _context.ResultMappings.Remove(item);
            await _context.SaveChangesAsync(cancellationToken);
            return Result.Success();
        }

        public async Task<Result> Handle(DeleteCheckedResultMappingsCommand request, CancellationToken cancellationToken)
        {

           var items = await _context.ResultMappings.Where(x => request.Id.Contains(x.Id)).ToListAsync(cancellationToken);
            foreach (var item in items)
            {
                _context.ResultMappings.Remove(item);
            }
            await _context.SaveChangesAsync(cancellationToken);
            return Result.Success();
        }
    }

