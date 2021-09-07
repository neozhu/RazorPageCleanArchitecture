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

namespace CleanArchitecture.Razor.Application.Projects.Commands.Create
{
    public class CreateProjectCommand: ProjectDto,IRequest<Result>, IMapFrom<Project>
    {
      
    }
    

    public class CreateProjectCommandHandler : IRequestHandler<CreateProjectCommand, Result>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly IStringLocalizer<CreateProjectCommand> _localizer;
        public CreateProjectCommandHandler(
            IApplicationDbContext context,
            IStringLocalizer<CreateProjectCommand> localizer,
            IMapper mapper
            )
        {
            _context = context;
            _localizer = localizer;
            _mapper = mapper;
        }
        public async Task<Result> Handle(CreateProjectCommand request, CancellationToken cancellationToken)
        {
           var item = _mapper.Map<Project>(request);
            _context.Projects.Add(item);
            await _context.SaveChangesAsync(cancellationToken);
            return  Result.Success();
        }
    }
}
