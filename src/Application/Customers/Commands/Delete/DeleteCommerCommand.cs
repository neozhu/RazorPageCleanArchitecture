// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using CleanArchitecture.Razor.Application.Common.Interfaces;
using CleanArchitecture.Razor.Application.Common.Mappings;
using CleanArchitecture.Razor.Application.Common.Models;
using CleanArchitecture.Razor.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CleanArchitecture.Razor.Application.Customers.Commands.Delete
{
   public class DeleteCommerCommand: IRequest<Result>
    {
        public int Id { get; set; }
    }
    public class DeleteCheckedCommersCommand : IRequest<Result>
    {
        public int[] Id { get; set; }
    }

    public class DeleteCustomerCommandHandler : IRequestHandler<DeleteCommerCommand, Result>,
        IRequestHandler<DeleteCheckedCommersCommand, Result>
    {
        private readonly IApplicationDbContext _context;

        public DeleteCustomerCommandHandler(
            IApplicationDbContext context
            )
        {
            _context = context;
        }
        public async Task<Result> Handle(DeleteCommerCommand request, CancellationToken cancellationToken)
        {
            var item =await _context.Customers.FindAsync(request.Id);
            _context.Customers.Remove(item);
            await _context.SaveChangesAsync(cancellationToken);
            return Result.Success();
        }

        public async Task<Result> Handle(DeleteCheckedCommersCommand request, CancellationToken cancellationToken)
        {
            var items = await _context.Customers.Where(x => request.Id.Contains(x.Id)).ToListAsync(cancellationToken);
            foreach(var item in items)
            {
                _context.Customers.Remove(item);
            }
            await _context.SaveChangesAsync(cancellationToken);
            return Result.Success();
        }
    }
}
