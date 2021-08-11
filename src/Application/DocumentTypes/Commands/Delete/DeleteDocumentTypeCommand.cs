// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using CleanArchitecture.Razor.Application.Common.Interfaces;
using CleanArchitecture.Razor.Application.Common.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CleanArchitecture.Razor.Application.DocumentTypes.Commands.Delete
{
    public class DeleteDocumentTypeCommand: IRequest<Result>
    {
        public int Id { get; set; }
    }
    public class DeleteCheckedDocumentTypesCommand : IRequest<Result>
    {
        public int[] Id { get; set; }
    }

    public class DeleteDocumentTypeCommandHandler : IRequestHandler<DeleteDocumentTypeCommand, Result>,
        IRequestHandler<DeleteCheckedDocumentTypesCommand, Result>
    {
        private readonly IApplicationDbContext _context;

        public DeleteDocumentTypeCommandHandler(
            IApplicationDbContext context
            )
        {
            _context = context;
        }
        public async Task<Result> Handle(DeleteDocumentTypeCommand request, CancellationToken cancellationToken)
        {
            var item =await _context.DocumentTypes.FindAsync(request.Id);
            _context.DocumentTypes.Remove(item);
            await _context.SaveChangesAsync(cancellationToken);
            return Result.Success();
        }

        public async Task<Result> Handle(DeleteCheckedDocumentTypesCommand request, CancellationToken cancellationToken)
        {
            var items = await _context.DocumentTypes.Where(x => request.Id.Contains(x.Id)).ToListAsync(cancellationToken);
            foreach(var item in items)
            {
                _context.DocumentTypes.Remove(item);
            }
            await _context.SaveChangesAsync(cancellationToken);
            return Result.Success();
        }
    }
}
