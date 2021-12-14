// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using CleanArchitecture.Razor.Application.ResultMappings.DTOs;

namespace CleanArchitecture.Razor.Application.ResultMappings.Commands.Update;

    public class UpdateResultMappingCommand: ResultMappingDto,IRequest<Result>, IMapFrom<ResultMapping>
    {
        
    }

    public class UpdateResultMappingCommandHandler : IRequestHandler<UpdateResultMappingCommand, Result>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly IStringLocalizer<UpdateResultMappingCommandHandler> _localizer;
        public UpdateResultMappingCommandHandler(
            IApplicationDbContext context,
            IStringLocalizer<UpdateResultMappingCommandHandler> localizer,
             IMapper mapper
            )
        {
            _context = context;
            _localizer = localizer;
            _mapper = mapper;
        }
        public async Task<Result> Handle(UpdateResultMappingCommand request, CancellationToken cancellationToken)
        {
         
           var item =await _context.ResultMappings.FindAsync( new object[] { request.Id }, cancellationToken);
           if (item != null)
           {
                item = _mapper.Map(request, item);
                await _context.SaveChangesAsync(cancellationToken);
           }
           return Result.Success();
        }
    }

