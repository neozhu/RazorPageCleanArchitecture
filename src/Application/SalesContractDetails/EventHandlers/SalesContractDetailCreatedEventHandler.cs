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

namespace CleanArchitecture.Razor.Application.SalesContractDetails.EventHandlers
{
    public class SalesContractDetailCreatedEventHandler : INotificationHandler<DomainEventNotification<SalesContractDetailCreatedEvent>>
    {
        private readonly IApplicationDbContext _context;
        private readonly ILogger<SalesContractDetailCreatedEventHandler> _logger;

        public SalesContractDetailCreatedEventHandler(
            IApplicationDbContext context,
            ILogger<SalesContractDetailCreatedEventHandler> logger
            )
        {
            _context = context;
            _logger = logger;
        }
        public async Task Handle(DomainEventNotification<SalesContractDetailCreatedEvent> notification, CancellationToken cancellationToken)
        {
            var domainEvent = notification.DomainEvent;

            var receiptamount = await _context.SalesContractDetails
                           .Where(x => x.SalesContractId == domainEvent.Item.SalesContractId)
                           .SumAsync(x => x.ReceiptAmount, cancellationToken);
           
            var contract = await _context.SalesContracts.FindAsync(new object[] { domainEvent.Item.SalesContractId }, cancellationToken);
            contract.ReceiptAmount = receiptamount ?? 0m;
            contract.Balance = contract.ContractAmount - contract.ReceiptAmount;
            if (contract.Balance <= 0)
            {
                contract.Status = "Closed";
                contract.ClosedDate = DateTime.Now;
            }
            else
            {
                contract.Status = "Pending";
            }
            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}
