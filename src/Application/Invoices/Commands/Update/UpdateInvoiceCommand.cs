using CleanArchitecture.Razor.Application.Invoices.DTOs;

namespace CleanArchitecture.Razor.Application.Invoices.Commands.Update
{
    public class UpdateInvoiceCommand: InvoiceDto,IRequest<Result>, IMapFrom<Invoice>
    {
        
    }

    public class UpdateInvoiceCommandHandler : IRequestHandler<UpdateInvoiceCommand, Result>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly IStringLocalizer<UpdateInvoiceCommandHandler> _localizer;
        public UpdateInvoiceCommandHandler(
            IApplicationDbContext context,
            IStringLocalizer<UpdateInvoiceCommandHandler> localizer,
             IMapper mapper
            )
        {
            _context = context;
            _localizer = localizer;
            _mapper = mapper;
        }
        public async Task<Result> Handle(UpdateInvoiceCommand request, CancellationToken cancellationToken)
        {
           //TODO:Implementing UpdateInvoiceCommandHandler method 
           var item =await _context.Invoices.FindAsync( new object[] { request.Id }, cancellationToken);
           if (item != null)
           {
                item = _mapper.Map(request, item);
                await _context.SaveChangesAsync(cancellationToken);
           }
           return Result.Success();
        }
    }
}
