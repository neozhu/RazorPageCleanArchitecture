using System.Linq;
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
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;

namespace CleanArchitecture.Razor.Application.Projects.Commands.Delete
{
    public class DeleteProjectCommand: IRequest<Result>
    {
      public int Id {  get; set; }
    }
    public class DeleteCheckedProjectsCommand : IRequest<Result>
    {
      public int[] Id {  get; set; }
    }

    public class DeleteProjectCommandHandler : 
                 IRequestHandler<DeleteProjectCommand, Result>,
                 IRequestHandler<DeleteCheckedProjectsCommand, Result>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly IStringLocalizer<DeleteProjectCommandHandler> _localizer;
        public DeleteProjectCommandHandler(
            IApplicationDbContext context,
            IStringLocalizer<DeleteProjectCommandHandler> localizer,
             IMapper mapper
            )
        {
            _context = context;
            _localizer = localizer;
            _mapper = mapper;
        }
        public async Task<Result> Handle(DeleteProjectCommand request, CancellationToken cancellationToken)
        {
            var item = await _context.Projects.FindAsync(new object[] { request.Id }, cancellationToken);
            _context.Projects.Remove(item);
            await _context.SaveChangesAsync(cancellationToken);
            return Result.Success();
        }

        public async Task<Result> Handle(DeleteCheckedProjectsCommand request, CancellationToken cancellationToken)
        {
           var items = await _context.Projects.Where(x => request.Id.Contains(x.Id)).ToListAsync(cancellationToken);
            foreach (var item in items)
            {
                _context.Projects.Remove(item);
            }
            await _context.SaveChangesAsync(cancellationToken);
            return Result.Success();
        }
    }
}
