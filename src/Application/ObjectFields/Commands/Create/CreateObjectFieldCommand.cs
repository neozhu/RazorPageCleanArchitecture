// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using CleanArchitecture.Razor.Application.ObjectFields.DTOs;

namespace CleanArchitecture.Razor.Application.ObjectFields.Commands.Create;

    public class CreateObjectFieldCommand: ObjectFieldDto,IRequest<Result<int>>, IMapFrom<ObjectField>
    {
       
    }
    
    public class CreateObjectFieldCommandHandler : IRequestHandler<CreateObjectFieldCommand, Result<int>>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly IStringLocalizer<CreateObjectFieldCommand> _localizer;
        public CreateObjectFieldCommandHandler(
            IApplicationDbContext context,
            IStringLocalizer<CreateObjectFieldCommand> localizer,
            IMapper mapper
            )
        {
            _context = context;
            _localizer = localizer;
            _mapper = mapper;
        }
        public async Task<Result<int>> Handle(CreateObjectFieldCommand request, CancellationToken cancellationToken)
        {

           var item = _mapper.Map<ObjectField>(request);
           _context.ObjectFields.Add(item);
           await _context.SaveChangesAsync(cancellationToken);
           return  Result<int>.Success(item.Id);
        }
    }

