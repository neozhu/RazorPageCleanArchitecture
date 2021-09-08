using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using CleanArchitecture.Razor.Application.Common.Interfaces;
using CleanArchitecture.Razor.Application.Common.Mappings;
using CleanArchitecture.Razor.Application.Common.Models;
using CleanArchitecture.Razor.Application.Projects.DTOs;
using CleanArchitecture.Razor.Domain.Entities;
using CleanArchitecture.Razor.Domain.Events;
using MediatR;
using Microsoft.Extensions.Localization;

namespace CleanArchitecture.Razor.Application.Projects.Commands.AddEdit
{
    public class AddEditProjectCommand: ProjectDto,IRequest<Result>, IMapFrom<Project>
    {
      
    }

    public class AddEditProjectCommandHandler : IRequestHandler<AddEditProjectCommand, Result>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly IStringLocalizer<AddEditProjectCommandHandler> _localizer;
        public AddEditProjectCommandHandler(
            IApplicationDbContext context,
            IStringLocalizer<AddEditProjectCommandHandler> localizer,
            IMapper mapper
            )
        {
            _context = context;
            _localizer = localizer;
            _mapper = mapper;
        }
        public async Task<Result> Handle(AddEditProjectCommand request, CancellationToken cancellationToken)
        {
            if (request.Id > 0)
            {
                var item = await _context.Projects.FindAsync(new object[] { request.Id }, cancellationToken);
                item = _mapper.Map(request, item);
            }
            else
            {
                var item = _mapper.Map<Project>(request);
                _context.Projects.Add(item);
            }
            await _context.SaveChangesAsync(cancellationToken);
            return Result.Success();
        }
    }
}
