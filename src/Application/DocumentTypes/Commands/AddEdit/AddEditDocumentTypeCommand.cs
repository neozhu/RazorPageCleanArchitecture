// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using CleanArchitecture.Razor.Application.Common.Interfaces;
using CleanArchitecture.Razor.Application.Common.Interfaces.Caching;
using CleanArchitecture.Razor.Application.Common.Mappings;
using CleanArchitecture.Razor.Application.Common.Models;
using CleanArchitecture.Razor.Application.DocumentTypes.Caching;
using CleanArchitecture.Razor.Application.DocumentTypes.DTOs;
using CleanArchitecture.Razor.Domain.Entities;
using CleanArchitecture.Razor.Domain.Enums;
using CleanArchitecture.Razor.Domain.Events;
using MediatR;

namespace CleanArchitecture.Razor.Application.DocumentTypes.Commands.AddEdit
{
    public class AddEditDocumentTypeCommand : DocumentTypeDto, IRequest<Result<int>>, IMapFrom<DocumentType>, ICacheInvalidator
    {
        public string CacheKey => string.Empty;

        public CancellationTokenSource ResetCacheToken => DocumentTypeCacheTokenSource.ResetCacheToken;
    }

    public class AddEditDocumentTypeCommandHandler : IRequestHandler<AddEditDocumentTypeCommand, Result<int>>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public AddEditDocumentTypeCommandHandler(
            IApplicationDbContext context,
             IMapper mapper
            )
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<Result<int>> Handle(AddEditDocumentTypeCommand request, CancellationToken cancellationToken)
        {

           
            if (request.Id > 0)
            {
                var documentType = await _context.DocumentTypes.FindAsync(request.Id);
                documentType = _mapper.Map(request, documentType);
                await _context.SaveChangesAsync(cancellationToken);
                return Result<int>.Success(documentType.Id);
            }
            else
            {
                var documentType = _mapper.Map<DocumentType>(request);
                _context.DocumentTypes.Add(documentType);
                await _context.SaveChangesAsync(cancellationToken);
                return Result<int>.Success(documentType.Id);
            }
            

        }
    }
}
