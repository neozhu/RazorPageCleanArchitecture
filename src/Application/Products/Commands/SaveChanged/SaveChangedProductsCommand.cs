// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using CleanArchitecture.Razor.Application.Common.Interfaces;
using CleanArchitecture.Razor.Application.Common.Mappings;
using CleanArchitecture.Razor.Application.Common.Models;
using CleanArchitecture.Razor.Application.Products.DTOs;
using CleanArchitecture.Razor.Application.Products.Queries;
using CleanArchitecture.Razor.Domain.Entities;
using CleanArchitecture.Razor.Domain.Enums;
using CleanArchitecture.Razor.Domain.Events;
using MediatR;

namespace CleanArchitecture.Razor.Application.Products.Commands.SaveChanged
{
    public class SaveChangedProductsCommand:IRequest<Result>
    {
      public ProductDto[] Items { get; set; }
    }

    public class SaveChangedProductsCommandHandler : IRequestHandler<SaveChangedProductsCommand, Result>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public SaveChangedProductsCommandHandler(
            IApplicationDbContext context,
             IMapper mapper
            )
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<Result> Handle(SaveChangedProductsCommand request, CancellationToken cancellationToken)
        {
            foreach(var item in request.Items)
            {
                switch (item.TrackingState)
                {
                    case TrackingState.Added:
                        var newitem = _mapper.Map<Product>(item);
                        await _context.Products.AddAsync(newitem, cancellationToken);
                        break;
                    case TrackingState.Deleted:
                        var delitem =await _context.Products.FindAsync(item.Id);
                        _context.Products.Remove(delitem);
                        break;
                    case TrackingState.Modified:
                        var edititem = await _context.Products.FindAsync(item.Id);
                        edititem.Name = item.Name;
                        edititem.Description = item.Description;
                        _context.Products.Update(edititem);
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
