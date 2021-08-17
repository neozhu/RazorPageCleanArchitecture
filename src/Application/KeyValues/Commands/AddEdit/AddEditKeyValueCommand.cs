// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using CleanArchitecture.Razor.Application.Common.Interfaces;
using CleanArchitecture.Razor.Application.Common.Mappings;
using CleanArchitecture.Razor.Application.Common.Models;
using CleanArchitecture.Razor.Application.KeyValues.DTOs;
using CleanArchitecture.Razor.Domain.Entities;
using CleanArchitecture.Razor.Domain.Enums;
using CleanArchitecture.Razor.Domain.Events;
using MediatR;

namespace CleanArchitecture.Razor.Application.KeyValues.Commands.AddEdit
{
    public class AddEditKeyValueCommand:KeyValueDto,IRequest<Result>, IMapFrom<KeyValue>
    {
       
         
    }

    public class AddEditKeyValueCommandHandler : IRequestHandler<AddEditKeyValueCommand, Result>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public AddEditKeyValueCommandHandler(
            IApplicationDbContext context,
             IMapper mapper
            )
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<Result> Handle(AddEditKeyValueCommand request, CancellationToken cancellationToken)
        {

           
            if (request.Id > 0)
            {
                var KeyValue = await _context.KeyValues.FindAsync(request.Id);
                KeyValue=_mapper.Map(request, KeyValue);
            }
            else
            {
                var KeyValue = _mapper.Map<KeyValue>(request);
                var createevent = new KeyValueCreatedEvent(KeyValue);
                KeyValue.DomainEvents.Add(createevent);
                _context.KeyValues.Add(KeyValue);
            }
            await _context.SaveChangesAsync(cancellationToken);
            return Result.Success();

        }
    }
}
