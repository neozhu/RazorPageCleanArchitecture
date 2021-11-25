// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.


using CleanArchitecture.Razor.Application.FieldMappingValues.DTOs;

namespace CleanArchitecture.Razor.Application.FieldMappingValues.Commands.AddEdit;

    public class AddEditFieldMappingValueCommand: FieldMappingValueDto,IRequest<Result<int>>, IMapFrom<FieldMappingValue>
    {
      
    }

    public class AddEditFieldMappingValueCommandHandler : IRequestHandler<AddEditFieldMappingValueCommand, Result<int>>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly IStringLocalizer<AddEditFieldMappingValueCommandHandler> _localizer;
        public AddEditFieldMappingValueCommandHandler(
            IApplicationDbContext context,
            IStringLocalizer<AddEditFieldMappingValueCommandHandler> localizer,
            IMapper mapper
            )
        {
            _context = context;
            _localizer = localizer;
            _mapper = mapper;
        }
        public async Task<Result<int>> Handle(AddEditFieldMappingValueCommand request, CancellationToken cancellationToken)
        {
            if (request.Id > 0)
            {
                var item = await _context.FieldMappingValues.FindAsync(new object[] { request.Id }, cancellationToken);
                item = _mapper.Map(request, item);
                await _context.SaveChangesAsync(cancellationToken);
                return Result<int>.Success(item.Id);
            }
            else
            {
                var item = _mapper.Map<FieldMappingValue>(request);
                _context.FieldMappingValues.Add(item);
                await _context.SaveChangesAsync(cancellationToken);
                return Result<int>.Success(item.Id);
            }
           
        }
    }

