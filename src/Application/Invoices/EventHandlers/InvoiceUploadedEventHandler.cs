using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using CleanArchitecture.Razor.Application.Common.Interfaces;
using CleanArchitecture.Razor.Application.Common.Models;
using CleanArchitecture.Razor.Domain.Events;
using MediatR;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using Hangfire;
using System;
using CleanArchitecture.Razor.Application.Invoices.PaddleOCR;

namespace CleanArchitecture.Razor.Application.Invoices.EventHandlers
{
    public class InvoiceUploadedEventHandler : INotificationHandler<DomainEventNotification<InvoiceUploadedEvent>>
    {
        private readonly IApplicationDbContext _context;
        private readonly IOcrJob _ocr;
        private readonly ILogger<InvoiceUploadedEventHandler> _logger;

        public InvoiceUploadedEventHandler(
            IApplicationDbContext context,
            IOcrJob ocr,
            ILogger<InvoiceUploadedEventHandler> logger
            )
        {
            _context = context;
            _ocr = ocr;
            _logger = logger;
        }
        public async Task Handle(DomainEventNotification<InvoiceUploadedEvent> notification, CancellationToken cancellationToken)
        {
            var domainEvent = notification.DomainEvent;
            var id = domainEvent.Item.Id;
            var item =await _context.Invoices.FindAsync(id);
            item.Status = "Queuing";
            await _context.SaveChangesAsync(cancellationToken);
            BackgroundJob.Enqueue(()=> _ocr.Recognition(domainEvent.Item.Id,domainEvent.Item.ImgString));

           
        }
    }
}
