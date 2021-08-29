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

namespace CleanArchitecture.Razor.Application.Features.ApprovalDatas.Queries.Export
{
    public class ExportApprovalDatasQuery : IRequest<byte[]>
    {
       
    }
    
    public class ExportApprovalDatasQueryHandler :
         IRequestHandler<ExportApprovalDatasQuery, byte[]>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly IStringLocalizer<ExportApprovalDatasQueryHandler> _localizer;

        public ExportApprovalDatasQueryHandler(
            IApplicationDbContext context,
            IMapper mapper,
            IStringLocalizer<ExportApprovalDatasQueryHandler> localizer
            )
        {
            _context = context;
            _mapper = mapper;
            _localizer = localizer;
        }

        public Task<byte[]> Handle(ExportApprovalDatasQuery request, CancellationToken cancellationToken)
        {
            //TODO:Implementing ExportApprovalDatasQueryHandler method 
            throw new NotImplementedException();
        }
    }
}

