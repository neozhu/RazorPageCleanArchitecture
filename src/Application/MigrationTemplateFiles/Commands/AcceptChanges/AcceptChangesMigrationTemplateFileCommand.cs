// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using CleanArchitecture.Razor.Application.MigrationTemplateFiles.DTOs;

namespace CleanArchitecture.Razor.Application.MigrationTemplateFiles.Commands.AcceptChanges;

    public class AcceptChangesMigrationTemplateFilesCommand:IRequest<Result>
    {
      public MigrationTemplateFileDto[] Items { get; set; }
    }

    public class AcceptChangesMigrationTemplateFilesCommandHandler : IRequestHandler<AcceptChangesMigrationTemplateFilesCommand, Result>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public AcceptChangesMigrationTemplateFilesCommandHandler(
            IApplicationDbContext context,
             IMapper mapper
            )
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<Result> Handle(AcceptChangesMigrationTemplateFilesCommand request, CancellationToken cancellationToken)
        {
            foreach(var item in request.Items)
            {
                switch (item.TrackingState)
                {
                    case TrackingState.Added:
                        var newitem = _mapper.Map<MigrationTemplateFile>(item);
                        await _context.MigrationTemplateFiles.AddAsync(newitem, cancellationToken);
                        break;
                    case TrackingState.Deleted:
                        var delitem =await _context.MigrationTemplateFiles.FindAsync(new object[] { item.Id }, cancellationToken);
                        _context.MigrationTemplateFiles.Remove(delitem);
                        break;
                    case TrackingState.Modified:
                        var edititem = await _context.MigrationTemplateFiles.FindAsync(new object[] { item.Id }, cancellationToken);
                    edititem = _mapper.Map(item, edititem);
                        _context.MigrationTemplateFiles.Update(edititem);
                        break;
                    case TrackingState.Unchanged:
                    default:
                        break;
                }
            }
            
            await _context.SaveChangesAsync(cancellationToken);
            return Result.Success();

        }
    }

