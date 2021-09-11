using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using CleanArchitecture.Razor.Application.Common.Interfaces;
using CleanArchitecture.Razor.Application.Common.Models;
using CleanArchitecture.Razor.Domain.Events;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace CleanArchitecture.Razor.Application.PurchaseContracts.EventHandlers
{
    public class PurchaseContractCreatedEventHandler : INotificationHandler<DomainEventNotification<PurchaseContractCreatedEvent>>
    {
        private readonly IApplicationDbContext _context;
        private readonly ILogger<PurchaseContractCreatedEventHandler> _logger;

        public PurchaseContractCreatedEventHandler(
            IApplicationDbContext context,
            ILogger<PurchaseContractCreatedEventHandler> logger
            )
        {
            _context = context;
            _logger = logger;
        }
        public async Task Handle(DomainEventNotification<PurchaseContractCreatedEvent> notification, CancellationToken cancellationToken)
        {
            var domainEvent = notification.DomainEvent;

            var cost = await _context.PurchaseContracts.Where(x => x.ProjectId == domainEvent.Item.ProjectId)
                     .SumAsync(x => x.ContractAmount, cancellationToken);
            var receipt = await _context.SalesContracts.Where(x => x.ProjectId == domainEvent.Item.ProjectId)
                    .SumAsync(x => x.ReceiptAmount, cancellationToken);
            var constractamount = await _context.SalesContracts.Where(x => x.ProjectId == domainEvent.Item.ProjectId)
                    .SumAsync(x => x.ContractAmount, cancellationToken);
            var project = await _context.Projects.FirstAsync(x => x.Id == domainEvent.Item.ProjectId,cancellationToken);
            project.ActualCost = cost;
            project.ReceiptAmount = receipt;
            project.ContractAmount = constractamount;
            if(project.ActualCost>0 && project.ContractAmount > 0)
            {
                project.GrossMargin = (project.ContractAmount - project.ActualCost)/project.ContractAmount;
            }
            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}
