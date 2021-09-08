using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using CleanArchitecture.Razor.Application.Common.Extensions;
using CleanArchitecture.Razor.Application.Common.Interfaces;
using CleanArchitecture.Razor.Domain.Entities;
using System.Linq.Dynamic.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;
using AutoMapper.QueryableExtensions;
using Microsoft.Extensions.Localization;
using CleanArchitecture.Razor.Application.Projects.DTOs;
using CleanArchitecture.Razor.Application.Models;
using CleanArchitecture.Razor.Application.Common.Mappings;

namespace CleanArchitecture.Razor.Application.Projects.Queries.Pagination
{
    public class ProjectsWithPaginationQuery : PaginationRequest, IRequest<PaginatedData<ProjectDto>>
    {
       
    }
    
    public class ProjectsWithPaginationQueryHandler :
         IRequestHandler<ProjectsWithPaginationQuery, PaginatedData<ProjectDto>>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly IStringLocalizer<ProjectsWithPaginationQueryHandler> _localizer;

        public ProjectsWithPaginationQueryHandler(
            IApplicationDbContext context,
            IMapper mapper,
            IStringLocalizer<ProjectsWithPaginationQueryHandler> localizer
            )
        {
            _context = context;
            _mapper = mapper;
            _localizer = localizer;
        }

        public async Task<PaginatedData<ProjectDto>> Handle(ProjectsWithPaginationQuery request, CancellationToken cancellationToken)
        {
          
           var filters = PredicateBuilder.FromFilter<Project>(request.FilterRules);
           var data = await _context.Projects.Where(filters)
                .OrderBy($"{request.Sort} {request.Order}")
                .ProjectTo<ProjectDto>(_mapper.ConfigurationProvider)
                .PaginatedDataAsync(request.Page, request.Rows);
            return data;
        }
    }
}

