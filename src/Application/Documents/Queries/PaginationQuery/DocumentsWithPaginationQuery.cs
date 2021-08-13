// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using CleanArchitecture.Razor.Application.Common.Extensions;
using CleanArchitecture.Razor.Application.Common.Interfaces;
using CleanArchitecture.Razor.Application.Common.Models;
using CleanArchitecture.Razor.Application.Models;
using CleanArchitecture.Razor.Domain.Entities;
using System.Linq.Dynamic.Core;
using MediatR;
using CleanArchitecture.Razor.Application.Common.Mappings;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using CleanArchitecture.Razor.Application.Documents.DTOs;

namespace CleanArchitecture.Razor.Application.Documents.Queries.PaginationQuery
{
    public class DocumentsWithPaginationQuery : IRequest<PaginatedData<DocumentDto>>
    {
        public string filterRules { get; set; }
        public int page { get; set; } = 1;
        public int rows { get; set; } = 15;
        public string sort { get; set; } = "Id";
        public string order { get; set; } = "desc";
        
    }
    public class DocumentsQueryHandler : IRequestHandler<DocumentsWithPaginationQuery, PaginatedData<DocumentDto>>
    {

        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public DocumentsQueryHandler(
        
            IApplicationDbContext context,
            IMapper mapper
            )
        {
    
            _context = context;
            _mapper = mapper;
        }
        public async Task<PaginatedData<DocumentDto>> Handle(DocumentsWithPaginationQuery request, CancellationToken cancellationToken)
        {
            var filters = PredicateBuilder.FromFilter<Document>(request.filterRules);
            var data = await _context.Documents.Where(filters)
                .OrderBy($"{request.sort} {request.order}")
                .ProjectTo<DocumentDto>(_mapper.ConfigurationProvider)
                .PaginatedDataAsync(request.page, request.rows);

            return data;
        }
    }
}
