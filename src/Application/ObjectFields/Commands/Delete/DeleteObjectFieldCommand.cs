// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using CleanArchitecture.Razor.Application.ObjectFields.DTOs;

namespace CleanArchitecture.Razor.Application.ObjectFields.Commands.Delete;

    public class DeleteObjectFieldCommand: IRequest<Result>
    {
      public int Id {  get; set; }
    }
    public class DeleteCheckedObjectFieldsCommand : IRequest<Result>
    {
      public int[] Id {  get; set; }
    }

    public class DeleteObjectFieldCommandHandler : 
                 IRequestHandler<DeleteObjectFieldCommand, Result>,
                 IRequestHandler<DeleteCheckedObjectFieldsCommand, Result>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly IStringLocalizer<DeleteObjectFieldCommandHandler> _localizer;
        public DeleteObjectFieldCommandHandler(
            IApplicationDbContext context,
            IStringLocalizer<DeleteObjectFieldCommandHandler> localizer,
             IMapper mapper
            )
        {
            _context = context;
            _localizer = localizer;
            _mapper = mapper;
        }
        public async Task<Result> Handle(DeleteObjectFieldCommand request, CancellationToken cancellationToken)
        {

           var item = await _context.ObjectFields.FindAsync(new object[] { request.Id }, cancellationToken);
            _context.ObjectFields.Remove(item);
            await _context.SaveChangesAsync(cancellationToken);
            return Result.Success();
        }

        public async Task<Result> Handle(DeleteCheckedObjectFieldsCommand request, CancellationToken cancellationToken)
        {

           var items = await _context.ObjectFields.Where(x => request.Id.Contains(x.Id)).ToListAsync(cancellationToken);
            foreach (var item in items)
            {
                _context.ObjectFields.Remove(item);
            }
            await _context.SaveChangesAsync(cancellationToken);
            return Result.Success();
        }
    }

