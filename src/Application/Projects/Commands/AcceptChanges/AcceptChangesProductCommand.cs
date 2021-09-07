// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using CleanArchitecture.Razor.Application.Common.Interfaces;
using CleanArchitecture.Razor.Application.Common.Models;
using CleanArchitecture.Razor.Application.Projects.DTOs;
using CleanArchitecture.Razor.Domain.Entities;
using CleanArchitecture.Razor.Domain.Enums;
using MediatR;

namespace CleanArchitecture.Razor.Application.Projects.Commands.AcceptChanges
{
    public class AcceptChangesProjectCommand:IRequest<Result>
    {
      public ProjectDto[] Items { get; set; }
    }

    public class AcceptChangesProjectCommandHandler : IRequestHandler<AcceptChangesProjectCommand, Result>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public AcceptChangesProjectCommandHandler(
            IApplicationDbContext context,
             IMapper mapper
            )
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<Result> Handle(AcceptChangesProjectCommand request, CancellationToken cancellationToken)
        {
            foreach(var item in request.Items)
            {
                switch (item.TrackingState)
                {
                    case TrackingState.Added:
                        var newitem = _mapper.Map<Project>(item);
                        await _context.Projects.AddAsync(newitem, cancellationToken);
                        break;
                    case TrackingState.Deleted:
                        var delitem =await _context.Projects.FindAsync(new object[] { item.Id }, cancellationToken);
                        _context.Projects.Remove(delitem);
                        break;
                    case TrackingState.Modified:
                        var edititem = await _context.Projects.FindAsync(new object[] { item.Id }, cancellationToken);
                        _mapper.Map(item, edititem);
                        _context.Projects.Update(edititem);
                        break;
                    case TrackingState.Unchanged:
                    default:
                        break;
                }
            }
            
            await _context.SaveChangesAsync(cancellationToken);
            return Result.Success();

        }
    }
}
