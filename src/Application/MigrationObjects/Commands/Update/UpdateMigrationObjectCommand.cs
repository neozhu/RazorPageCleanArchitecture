// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using CleanArchitecture.Razor.Application.MigrationObjects.DTOs;

namespace CleanArchitecture.Razor.Application.MigrationObjects.Commands.Update;

    public class UpdateMigrationObjectCommand: MigrationObjectDto,IRequest<Result>, IMapFrom<MigrationObject>
    {
        
    }

    public class UpdateMigrationObjectCommandHandler : IRequestHandler<UpdateMigrationObjectCommand, Result>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly IStringLocalizer<UpdateMigrationObjectCommandHandler> _localizer;
        public UpdateMigrationObjectCommandHandler(
            IApplicationDbContext context,
            IStringLocalizer<UpdateMigrationObjectCommandHandler> localizer,
             IMapper mapper
            )
        {
            _context = context;
            _localizer = localizer;
            _mapper = mapper;
        }
        public async Task<Result> Handle(UpdateMigrationObjectCommand request, CancellationToken cancellationToken)
        {
           //TODO:Implementing UpdateMigrationObjectCommandHandler method 
           var item =await _context.MigrationObjects.FindAsync( new object[] { request.Id }, cancellationToken);
           if (item != null)
           {
                item = _mapper.Map(request, item);
                await _context.SaveChangesAsync(cancellationToken);
           }
           return Result.Success();
        }
    }

