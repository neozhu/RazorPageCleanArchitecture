using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using CleanArchitecture.Razor.Application.Common.Interfaces;
using CleanArchitecture.Razor.Application.Common.Models;
using CleanArchitecture.Razor.Domain.Events;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace CleanArchitecture.Razor.Application.PurchaseContractDetails.EventHandlers
{
    public class PurchaseContractDetailDeletedEventHandler : INotificationHandler<DomainEventNotification<PurchaseContractDetailDeletedEvent>>
    {
        private readonly IApplicationDbContext _context;
        private readonly ILogger<PurchaseContractDetailDeletedEventHandler> _logger;

        public PurchaseContractDetailDeletedEventHandler(
            IApplicationDbContext context,
            ILogger<PurchaseContractDetailDeletedEventHandler> logger
            )
        {
            _context = context;
            _logger = logger;
        }
        public async Task Handle(DomainEventNotification<PurchaseContractDetailDeletedEvent> notification, CancellationToken cancellationToken)
        {
            var domainEvent = notification.DomainEvent;

            var paidamount = await _context.PurchaseContractDetails
                           .Where(x => x.PurchaseContractId == domainEvent.Item.PurchaseContractId)
                           .SumAsync(x => x.PaidAmount, cancellationToken);
            var invamount = await _context.PurchaseContractDetails
                           .Where(x => x.PurchaseContractId == domainEvent.Item.PurchaseContractId && x.InvoiceDate != null)
                           .SumAsync(x => x.PaidAmount, cancellationToken);
            var purchase = await _context.PurchaseContracts.FindAsync(new object[] { domainEvent.Item.PurchaseContractId }, cancellationToken);

            purchase.PaidAmount = paidamount ?? 0m;
            purchase.InvoiceAmount = invamount ?? 0m;
            purchase.Balance = purchase.ContractAmount - purchase.PaidAmount;
            purchase.DomainEvents.Add(new PurchaseContractUpdatedEvent(purchase));
            if (purchase.Balance <= 0)
            {
                purchase.Status = "Closed";
                purchase.ClosedDate = DateTime.Now;
            }
            else
            {
                purchase.Status = "Pending";
            }
            await _context.SaveChangesAsync(cancellationToken);
            _logger.LogInformation($"Update PurchaseContracts:ContractAmount:{ purchase.ContractAmount},PaidAmount:{ purchase.PaidAmount},InvoiceAmount:{ purchase.InvoiceAmount},Balance:{purchase.Balance} ");
        }
    }
}
