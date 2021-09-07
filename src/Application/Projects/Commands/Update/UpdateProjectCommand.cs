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

namespace CleanArchitecture.Razor.Application.Projects.Commands.Update
{
    public class UpdateProjectCommand: ProjectDto,IRequest<Result>, IMapFrom<Project>
    {
        public void Mapping(Profile profile)
        {
            profile.CreateMap<Project, UpdateProjectCommand>().ReverseMap();
        }
    }

    public class UpdateProjectCommandHandler : IRequestHandler<UpdateProjectCommand, Result>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly IStringLocalizer<UpdateProjectCommandHandler> _localizer;
        public UpdateProjectCommandHandler(
            IApplicationDbContext context,
            IStringLocalizer<UpdateProjectCommandHandler> localizer,
             IMapper mapper
            )
        {
            _context = context;
            _localizer = localizer;
            _mapper = mapper;
        }
        public async Task<Result> Handle(UpdateProjectCommand request, CancellationToken cancellationToken)
        {
           var item =await _context.Projects.FindAsync( new object[] { request.Id }, cancellationToken);
           if (item != null)
           {
                item = _mapper.Map(request, item);
                await _context.SaveChangesAsync(cancellationToken);
           }
           return Result.Success();
        }
    }
}
