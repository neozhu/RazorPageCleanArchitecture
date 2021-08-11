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
using CleanArchitecture.Razor.Application.Common.Mappings;
using CleanArchitecture.Razor.Application.Common.Models;
using CleanArchitecture.Razor.Domain.Entities;
using CleanArchitecture.Razor.Domain.Enums;
using CleanArchitecture.Razor.Domain.Events;
using MediatR;

namespace CleanArchitecture.Razor.Application.DocumentTypes.Commands.AddEdit
{
    public class AddEditDocumentTypeCommand:IRequest<Result>, IMapFrom<DocumentType>
    {
        public void Mapping(Profile profile)
        {
            profile.CreateMap<DocumentType, AddEditDocumentTypeCommand>().ReverseMap();
    
        }
        public int Id { get; set; }
        public string Name { get; set; }
        
        public string Description { get; set; }
    }

    public class AddEditDocumentTypeCommandHandler : IRequestHandler<AddEditDocumentTypeCommand, Result>
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
        public async Task<Result> Handle(AddEditDocumentTypeCommand request, CancellationToken cancellationToken)
        {

           
            if (request.Id > 0)
            {
                var DocumentType = await _context.DocumentTypes.FindAsync(request.Id);
                DocumentType=_mapper.Map(request, DocumentType);
            }
            else
            {
                var DocumentType = _mapper.Map<DocumentType>(request);
                _context.DocumentTypes.Add(DocumentType);
            }
            await _context.SaveChangesAsync(cancellationToken);
            return Result.Success();

        }
    }
}
