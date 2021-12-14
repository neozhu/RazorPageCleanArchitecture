// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using CleanArchitecture.Razor.Application.ResultMappings.DTOs;

namespace CleanArchitecture.Razor.Application.ResultMappings.Commands.Create;

    public class CreateResultMappingCommand: ResultMappingDto,IRequest<Result<int>>, IMapFrom<ResultMapping>
    {
    public UploadRequest UploadRequest { get; set; }
}
    
    public class CreateResultMappingCommandHandler : IRequestHandler<CreateResultMappingCommand, Result<int>>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly IStringLocalizer<CreateResultMappingCommand> _localizer;
        public CreateResultMappingCommandHandler(
            IApplicationDbContext context,
            IStringLocalizer<CreateResultMappingCommand> localizer,
            IMapper mapper
            )
        {
            _context = context;
            _localizer = localizer;
            _mapper = mapper;
        }
        public async Task<Result<int>> Handle(CreateResultMappingCommand request, CancellationToken cancellationToken)
        {
  
           var item = _mapper.Map<ResultMapping>(request);
           _context.ResultMappings.Add(item);
           await _context.SaveChangesAsync(cancellationToken);
           return  Result<int>.Success(item.Id);
        }
    }

