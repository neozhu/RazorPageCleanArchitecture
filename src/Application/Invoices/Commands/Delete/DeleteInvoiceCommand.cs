using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using CleanArchitecture.Razor.Application.Common.Interfaces;
using CleanArchitecture.Razor.Application.Common.Mappings;
using CleanArchitecture.Razor.Application.Common.Models;
using CleanArchitecture.Razor.Application.Invoices.DTOs;
using CleanArchitecture.Razor.Domain.Entities;
using CleanArchitecture.Razor.Domain.Events;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;

namespace CleanArchitecture.Razor.Application.Invoices.Commands.Delete
{
    public class DeleteInvoiceCommand: IRequest<Result>
    {
      public int Id {  get; set; }
    }
    public class DeleteCheckedInvoicesCommand : IRequest<Result>
    {
      public int[] Id {  get; set; }
    }

    public class DeleteInvoiceCommandHandler : 
                 IRequestHandler<DeleteInvoiceCommand, Result>,
                 IRequestHandler<DeleteCheckedInvoicesCommand, Result>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly IStringLocalizer<DeleteInvoiceCommandHandler> _localizer;
        public DeleteInvoiceCommandHandler(
            IApplicationDbContext context,
            IStringLocalizer<DeleteInvoiceCommandHandler> localizer,
             IMapper mapper
            )
        {
            _context = context;
            _localizer = localizer;
            _mapper = mapper;
        }
        public async Task<Result> Handle(DeleteInvoiceCommand request, CancellationToken cancellationToken)
        {
           var item = await _context.Invoices.FindAsync(new object[] { request.Id }, cancellationToken);
            _context.Invoices.Remove(item);
            await _context.SaveChangesAsync(cancellationToken);
            return Result.Success();
        }

        public async Task<Result> Handle(DeleteCheckedInvoicesCommand request, CancellationToken cancellationToken)
        {
           var items = await _context.Invoices.Where(x => request.Id.Contains(x.Id)).ToListAsync(cancellationToken);
            foreach (var item in items)
            {
                _context.Invoices.Remove(item);
            }
            await _context.SaveChangesAsync(cancellationToken);
            return Result.Success();
        }
    }
}
