// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using CleanArchitecture.Razor.Application.MigrationObjects.DTOs;

namespace CleanArchitecture.Razor.Application.MigrationObjects.Commands.Create;

    public class CreateMigrationObjectCommand: MigrationObjectDto,IRequest<Result<int>>, IMapFrom<MigrationObject>
    {
       
    }
    
    public class CreateMigrationObjectCommandHandler : IRequestHandler<CreateMigrationObjectCommand, Result<int>>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly IStringLocalizer<CreateMigrationObjectCommand> _localizer;
        public CreateMigrationObjectCommandHandler(
            IApplicationDbContext context,
            IStringLocalizer<CreateMigrationObjectCommand> localizer,
            IMapper mapper
            )
        {
            _context = context;
            _localizer = localizer;
            _mapper = mapper;
        }
        public async Task<Result<int>> Handle(CreateMigrationObjectCommand request, CancellationToken cancellationToken)
        {

           var item = _mapper.Map<MigrationObject>(request);
           _context.MigrationObjects.Add(item);
           await _context.SaveChangesAsync(cancellationToken);
           return  Result<int>.Success(item.Id);
        }
    }

