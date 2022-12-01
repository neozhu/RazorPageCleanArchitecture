using Hangfire;
using CleanArchitecture.Razor.Application.Invoices.PaddleOCR;

namespace CleanArchitecture.Razor.Application.Invoices.EventHandlers
{
    public class InvoiceUploadedEventHandler : INotificationHandler<DomainEventNotification<InvoiceUploadedEvent>>
    {
        private readonly IApplicationDbContext _context;
        private readonly IInvoicesOcrJob _ocr;
        private readonly ILogger<InvoiceUploadedEventHandler> _logger;

        public InvoiceUploadedEventHandler(
            IApplicationDbContext context,
            IInvoicesOcrJob ocr,
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
            var item =await _context.Invoices.FindAsync(new object[] { id }, cancellationToken);
            item.Status = "Queuing";
            await _context.SaveChangesAsync(cancellationToken);
            BackgroundJob.Enqueue(()=> _ocr.Recognition(domainEvent.Item.Id, cancellationToken));

           
        }
    }
}
