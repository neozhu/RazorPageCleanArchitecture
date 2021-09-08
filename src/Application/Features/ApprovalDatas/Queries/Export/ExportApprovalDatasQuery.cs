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
using CleanArchitecture.Razor.Application.Features.ApprovalDatas.DTOs;
using CleanArchitecture.Razor.Domain.Entities.Worflow;

namespace CleanArchitecture.Razor.Application.Features.ApprovalDatas.Queries.Export
{
    public class ExportApprovalDatasQuery : IRequest<byte[]>
    {
        public string filterRules { get; set; }
        public string sort { get; set; } = "Id";
        public string order { get; set; } = "desc";
    }

    public class ExportApprovalDatasQueryHandler :
         IRequestHandler<ExportApprovalDatasQuery, byte[]>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly IExcelService _excelService;
        private readonly IStringLocalizer<ExportApprovalDatasQueryHandler> _localizer;

        public ExportApprovalDatasQueryHandler(
            IApplicationDbContext context,
            IMapper mapper,
            IExcelService excelService,
            IStringLocalizer<ExportApprovalDatasQueryHandler> localizer
            )
        {
            _context = context;
            _mapper = mapper;
            _excelService = excelService;
            _localizer = localizer;
        }

        public async Task<byte[]> Handle(ExportApprovalDatasQuery request, CancellationToken cancellationToken)
        {
            var filters = PredicateBuilder.FromFilter<ApprovalData>(request.filterRules);
            var data = await _context.ApprovalDatas
                .Where(x => x.Status == "Finished")
                .Where(filters)
                .OrderBy($"{request.sort} {request.order}")
                .ProjectTo<ApprovalDataDto>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);
            var result = await _excelService.ExportAsync(data,
                new Dictionary<string, Func<ApprovalDataDto, object>>()
                {
                    //{ _localizer["Id"], item => item.Id },
                    { _localizer["Workflow Name"], item => item.WorkflowName },
                    { _localizer["Status"], item => item.Status },
                    { _localizer["Document Name"], item => item.DocumentName },
                    { _localizer["Url"], item => item.Url },
                    { _localizer["Applicant"], item => item.Applicant },
                    { _localizer["Request DateTime"], item => item.RequestDateTime.ToString("yyyy-MM-dd HH:mm:ss") },
                    { _localizer["Approver"], item => item.Approver },
                    { _localizer["Outcome"], item => item.Outcome },
                    { _localizer["Comments"], item => item.Comments },
                    { _localizer["Approved DateTime"], item => item.ApprovedDateTime?.ToString("yyyy-MM-dd HH:mm:ss") }
                }, _localizer["ApprovalHistories"]
                );
            return result;
        }
    }
}

