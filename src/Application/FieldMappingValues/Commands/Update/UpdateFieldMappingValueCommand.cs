// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using CleanArchitecture.Razor.Application.FieldMappingValues.DTOs;

namespace CleanArchitecture.Razor.Application.FieldMappingValues.Commands.Update;

    public class UpdateFieldMappingValueCommand: FieldMappingValueDto,IRequest<Result>, IMapFrom<FieldMappingValue>
    {
        
    }

    public class UpdateFieldMappingValueCommandHandler : IRequestHandler<UpdateFieldMappingValueCommand, Result>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly IStringLocalizer<UpdateFieldMappingValueCommandHandler> _localizer;
        public UpdateFieldMappingValueCommandHandler(
            IApplicationDbContext context,
            IStringLocalizer<UpdateFieldMappingValueCommandHandler> localizer,
             IMapper mapper
            )
        {
            _context = context;
            _localizer = localizer;
            _mapper = mapper;
        }
        public async Task<Result> Handle(UpdateFieldMappingValueCommand request, CancellationToken cancellationToken)
        {
            var item =await _context.FieldMappingValues.FindAsync( new object[] { request.Id }, cancellationToken);
           if (item != null)
           {
                item = _mapper.Map(request, item);
                await _context.SaveChangesAsync(cancellationToken);
           }
           return Result.Success();
        }
    }

