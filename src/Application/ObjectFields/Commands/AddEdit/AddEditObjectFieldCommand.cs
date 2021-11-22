// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.


using CleanArchitecture.Razor.Application.ObjectFields.DTOs;

namespace CleanArchitecture.Razor.Application.ObjectFields.Commands.AddEdit;

    public class AddEditObjectFieldCommand: ObjectFieldDto,IRequest<Result<int>>, IMapFrom<ObjectField>
    {
      
    }

    public class AddEditObjectFieldCommandHandler : IRequestHandler<AddEditObjectFieldCommand, Result<int>>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly IStringLocalizer<AddEditObjectFieldCommandHandler> _localizer;
        public AddEditObjectFieldCommandHandler(
            IApplicationDbContext context,
            IStringLocalizer<AddEditObjectFieldCommandHandler> localizer,
            IMapper mapper
            )
        {
            _context = context;
            _localizer = localizer;
            _mapper = mapper;
        }
        public async Task<Result<int>> Handle(AddEditObjectFieldCommand request, CancellationToken cancellationToken)
        {

            if (request.Id > 0)
            {
                var item = await _context.ObjectFields.FindAsync(new object[] { request.Id }, cancellationToken);
                item = _mapper.Map(request, item);
                await _context.SaveChangesAsync(cancellationToken);
                return Result<int>.Success(item.Id);
            }
            else
            {
                var item = _mapper.Map<ObjectField>(request);
                _context.ObjectFields.Add(item);
                await _context.SaveChangesAsync(cancellationToken);
                return Result<int>.Success(item.Id);
            }
           
        }
    }

