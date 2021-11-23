// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using CleanArchitecture.Razor.Application.FieldValueMappings.DTOs;

namespace CleanArchitecture.Razor.Application.FieldValueMappings.Commands.Delete;

    public class DeleteFieldValueMappingCommand: IRequest<Result>
    {
      public int Id {  get; set; }
    }
    public class DeleteCheckedFieldValueMappingsCommand : IRequest<Result>
    {
      public int[] Id {  get; set; }
    }

    public class DeleteFieldValueMappingCommandHandler : 
                 IRequestHandler<DeleteFieldValueMappingCommand, Result>,
                 IRequestHandler<DeleteCheckedFieldValueMappingsCommand, Result>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly IStringLocalizer<DeleteFieldValueMappingCommandHandler> _localizer;
        public DeleteFieldValueMappingCommandHandler(
            IApplicationDbContext context,
            IStringLocalizer<DeleteFieldValueMappingCommandHandler> localizer,
             IMapper mapper
            )
        {
            _context = context;
            _localizer = localizer;
            _mapper = mapper;
        }
        public async Task<Result> Handle(DeleteFieldValueMappingCommand request, CancellationToken cancellationToken)
        {
           
           var item = await _context.FieldValueMappings.FindAsync(new object[] { request.Id }, cancellationToken);
            _context.FieldValueMappings.Remove(item);
            await _context.SaveChangesAsync(cancellationToken);
            return Result.Success();
        }

        public async Task<Result> Handle(DeleteCheckedFieldValueMappingsCommand request, CancellationToken cancellationToken)
        {
           
           var items = await _context.FieldValueMappings.Where(x => request.Id.Contains(x.Id)).ToListAsync(cancellationToken);
            foreach (var item in items)
            {
                _context.FieldValueMappings.Remove(item);
            }
            await _context.SaveChangesAsync(cancellationToken);
            return Result.Success();
        }
    }

