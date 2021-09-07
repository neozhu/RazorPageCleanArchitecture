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

namespace CleanArchitecture.Razor.Application.Projects.Queries.Export
{
    public class ExportProjectsQuery : IRequest<byte[]>
    {
        public string FilterRules { get; set; }
        public string Sort { get; set; } = "Id";
        public string Order { get; set; } = "desc";
    }
    
    public class ExportProjectsQueryHandler :
         IRequestHandler<ExportProjectsQuery, byte[]>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly IExcelService _excelService;
        private readonly IStringLocalizer<ExportProjectsQueryHandler> _localizer;

        public ExportProjectsQueryHandler(
            IApplicationDbContext context,
            IMapper mapper,
            IExcelService excelService,
            IStringLocalizer<ExportProjectsQueryHandler> localizer
            )
        {
            _context = context;
            _mapper = mapper;
            _excelService = excelService;
            _localizer = localizer;
        }

        public async Task<byte[]> Handle(ExportProjectsQuery request, CancellationToken cancellationToken)
        {
            var filters = PredicateBuilder.FromFilter<Project>(request.FilterRules);
            var data = await _context.Projects.Where(filters)
                       .OrderBy($"{request.Sort} {request.Order}")
                       .ProjectTo<ProjectDto>(_mapper.ConfigurationProvider)
                       .ToListAsync(cancellationToken);
            var result = await _excelService.ExportAsync(data,
                new Dictionary<string, Func<ProjectDto, object>>()
                {
                    //{ _localizer["Id"], item => item.Id },
                    { _localizer["Name"], item => item.Name },
                    { _localizer["Status"], item => item.Status },
                    { _localizer["Description"], item => item.Description },
                    { _localizer["Begin DateTime"], item => item.BeginDateTime.ToString("yyyy-MM-dd") },
                    { _localizer["End DateTime"], item => item.EndDateTime?.ToString("yyyy-MM-dd") },
                    { _localizer["Estimated Cost"], item => item.EstimatedCost },
                    { _localizer["Actual Cost"], item => item.ActualCost },
                    { _localizer["Contract Amount"], item => item.ContractAmount },
                    { _localizer["Receipt Amount"], item => item.ReceiptAmount },
                    { _localizer["Gross Margin"], item => item.GrossMargin },
                }
                , _localizer["Projects"]);
            return result;
        }
    }
}

