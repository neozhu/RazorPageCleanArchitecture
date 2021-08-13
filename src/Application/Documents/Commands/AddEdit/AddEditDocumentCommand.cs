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
using CleanArchitecture.Razor.Application.Models;
using CleanArchitecture.Razor.Domain.Entities;
using CleanArchitecture.Razor.Domain.Enums;
using CleanArchitecture.Razor.Domain.Events;
using MediatR;

namespace CleanArchitecture.Razor.Application.Documents.Commands.AddEdit
{
    public class AddEditDocumentCommand:IRequest<Result>, IMapFrom<Document>
    {
        public void Mapping(Profile profile)
        {
            profile.CreateMap<Document, AddEditDocumentCommand>().ReverseMap();
    
        }
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public bool IsPublic { get; set; } = false;
        public string URL { get; set; }
        public int DocumentTypeId { get; set; }
        public UploadRequest UploadRequest { get; set; }
    }

    public class AddEditDocumentCommandHandler : IRequestHandler<AddEditDocumentCommand, Result>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public AddEditDocumentCommandHandler(
            IApplicationDbContext context,
             IMapper mapper
            )
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<Result> Handle(AddEditDocumentCommand request, CancellationToken cancellationToken)
        {

           
            if (request.Id > 0)
            {
                var Document = await _context.Documents.FindAsync(request.Id);
                Document=_mapper.Map(request, Document);
            }
            else
            {
                var Document = _mapper.Map<Document>(request);
                _context.Documents.Add(Document);
            }
            await _context.SaveChangesAsync(cancellationToken);
            return Result.Success();

        }
    }
}
