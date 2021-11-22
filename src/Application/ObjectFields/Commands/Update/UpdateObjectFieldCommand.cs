// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using CleanArchitecture.Razor.Application.ObjectFields.DTOs;

namespace CleanArchitecture.Razor.Application.ObjectFields.Commands.Update;

    public class UpdateObjectFieldCommand: ObjectFieldDto,IRequest<Result>, IMapFrom<ObjectField>
    {
        
    }

    public class UpdateObjectFieldCommandHandler : IRequestHandler<UpdateObjectFieldCommand, Result>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly IStringLocalizer<UpdateObjectFieldCommandHandler> _localizer;
        public UpdateObjectFieldCommandHandler(
            IApplicationDbContext context,
            IStringLocalizer<UpdateObjectFieldCommandHandler> localizer,
             IMapper mapper
            )
        {
            _context = context;
            _localizer = localizer;
            _mapper = mapper;
        }
        public async Task<Result> Handle(UpdateObjectFieldCommand request, CancellationToken cancellationToken)
        {

           var item =await _context.ObjectFields.FindAsync( new object[] { request.Id }, cancellationToken);
           if (item != null)
           {
                item = _mapper.Map(request, item);
                await _context.SaveChangesAsync(cancellationToken);
           }
           return Result.Success();
        }
    }

