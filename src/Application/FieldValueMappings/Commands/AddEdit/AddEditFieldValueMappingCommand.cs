// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.


using CleanArchitecture.Razor.Application.FieldValueMappings.DTOs;

namespace CleanArchitecture.Razor.Application.FieldValueMappings.Commands.AddEdit;

    public class AddEditFieldValueMappingCommand: FieldValueMappingDto,IRequest<Result<int>>, IMapFrom<FieldValueMapping>
    {
      
    }

    public class AddEditFieldValueMappingCommandHandler : IRequestHandler<AddEditFieldValueMappingCommand, Result<int>>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly IStringLocalizer<AddEditFieldValueMappingCommandHandler> _localizer;
        public AddEditFieldValueMappingCommandHandler(
            IApplicationDbContext context,
            IStringLocalizer<AddEditFieldValueMappingCommandHandler> localizer,
            IMapper mapper
            )
        {
            _context = context;
            _localizer = localizer;
            _mapper = mapper;
        }
        public async Task<Result<int>> Handle(AddEditFieldValueMappingCommand request, CancellationToken cancellationToken)
        {
            if (request.Id > 0)
            {
                var item = await _context.FieldValueMappings.FindAsync(new object[] { request.Id }, cancellationToken);
                item = _mapper.Map(request, item);
                await _context.SaveChangesAsync(cancellationToken);
                return Result<int>.Success(item.Id);
            }
            else
            {
                var item = _mapper.Map<FieldValueMapping>(request);
                _context.FieldValueMappings.Add(item);
                await _context.SaveChangesAsync(cancellationToken);
                return Result<int>.Success(item.Id);
            }
           
        }
    }

