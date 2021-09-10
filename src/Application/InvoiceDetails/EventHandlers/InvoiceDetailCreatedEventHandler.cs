using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using CleanArchitecture.Razor.Application.Common.Interfaces;
using CleanArchitecture.Razor.Application.Common.Models;
using CleanArchitecture.Razor.Domain.Events;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace CleanArchitecture.Razor.Application.InvoiceDetails.EventHandlers
{
    public class InvoiceDetailCreatedEventHandler : INotificationHandler<DomainEventNotification<InvoiceDetailCreatedEvent>>
    {
        private readonly IApplicationDbContext _context;
        private readonly ILogger<InvoiceDetailCreatedEventHandler> _logger;

        public InvoiceDetailCreatedEventHandler(
            IApplicationDbContext context,
            ILogger<InvoiceDetailCreatedEventHandler> logger
            )
        {
            _context = context;
            _logger = logger;
        }
        public async Task Handle(DomainEventNotification<InvoiceDetailCreatedEvent> notification, CancellationToken cancellationToken)
        {
            var domainEvent = notification.DomainEvent;
            var amount = await _context.InvoiceDetails
                                       .Where(x => x.SalesContractId == domainEvent.Item.SalesContractId)
                                       .SumAsync(x => x.InvoiceAmount, cancellationToken);

            var contract = await _context.SalesContracts.FindAsync(new object[] { domainEvent.Item.SalesContractId }, cancellationToken);
            contract.InvoiceAmount = amount ;
            await _context.SaveChangesAsync(cancellationToken); 
        }
    }
}
