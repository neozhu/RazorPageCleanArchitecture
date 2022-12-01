using CleanArchitecture.Razor.Application.Invoices.DTOs;

namespace CleanArchitecture.Razor.Application.Invoices.Commands.AddEdit
{
    public class AddEditInvoiceCommand: InvoiceDto,IRequest<Result<int>>, IMapFrom<Invoice>
    {
      
    }

    public class AddEditInvoiceCommandHandler : IRequestHandler<AddEditInvoiceCommand, Result<int>>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly IStringLocalizer<AddEditInvoiceCommandHandler> _localizer;
        public AddEditInvoiceCommandHandler(
            IApplicationDbContext context,
            IStringLocalizer<AddEditInvoiceCommandHandler> localizer,
            IMapper mapper
            )
        {
            _context = context;
            _localizer = localizer;
            _mapper = mapper;
        }
        public async Task<Result<int>> Handle(AddEditInvoiceCommand request, CancellationToken cancellationToken)
        {
            if (request.Id > 0)
            {
                var item = await _context.Invoices.FindAsync(new object[] { request.Id }, cancellationToken);
                item = _mapper.Map(request, item);
                await _context.SaveChangesAsync(cancellationToken);
                return Result<int>.Success(item.Id);
            }
            else
            {
                var item = _mapper.Map<Invoice>(request);
                _context.Invoices.Add(item);
                await _context.SaveChangesAsync(cancellationToken);
                return Result<int>.Success(item.Id);
            }
           
        }
    }
}
