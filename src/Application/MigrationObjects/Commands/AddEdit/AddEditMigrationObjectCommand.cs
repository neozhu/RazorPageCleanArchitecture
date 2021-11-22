// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.


using CleanArchitecture.Razor.Application.MigrationObjects.DTOs;

namespace CleanArchitecture.Razor.Application.MigrationObjects.Commands.AddEdit;

    public class AddEditMigrationObjectCommand: MigrationObjectDto,IRequest<Result<int>>, IMapFrom<MigrationObject>
    {
      
    }

    public class AddEditMigrationObjectCommandHandler : IRequestHandler<AddEditMigrationObjectCommand, Result<int>>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly IStringLocalizer<AddEditMigrationObjectCommandHandler> _localizer;
        public AddEditMigrationObjectCommandHandler(
            IApplicationDbContext context,
            IStringLocalizer<AddEditMigrationObjectCommandHandler> localizer,
            IMapper mapper
            )
        {
            _context = context;
            _localizer = localizer;
            _mapper = mapper;
        }
        public async Task<Result<int>> Handle(AddEditMigrationObjectCommand request, CancellationToken cancellationToken)
        {
 
            if (request.Id > 0)
            {
                var item = await _context.MigrationObjects.FindAsync(new object[] { request.Id }, cancellationToken);
                item = _mapper.Map(request, item);
                await _context.SaveChangesAsync(cancellationToken);
                return Result<int>.Success(item.Id);
            }
            else
            {
                var item = _mapper.Map<MigrationObject>(request);
                _context.MigrationObjects.Add(item);
                await _context.SaveChangesAsync(cancellationToken);
                return Result<int>.Success(item.Id);
            }
           
        }
    }

