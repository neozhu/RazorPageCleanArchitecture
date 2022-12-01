using CleanArchitecture.Razor.Application.Invoices.DTOs;

namespace CleanArchitecture.Razor.Application.Invoices.Queries.GetAll
{
    public class GetAllInvoicesQuery : IRequest<IEnumerable<InvoiceDto>>
    {
       
    }
    
    public class GetAllInvoicesQueryHandler :
         IRequestHandler<GetAllInvoicesQuery, IEnumerable<InvoiceDto>>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly IStringLocalizer<GetAllInvoicesQueryHandler> _localizer;

        public GetAllInvoicesQueryHandler(
            IApplicationDbContext context,
            IMapper mapper,
            IStringLocalizer<GetAllInvoicesQueryHandler> localizer
            )
        {
            _context = context;
            _mapper = mapper;
            _localizer = localizer;
        }

        public async Task<IEnumerable<InvoiceDto>> Handle(GetAllInvoicesQuery request, CancellationToken cancellationToken)
        {
            //TODO:Implementing GetAllInvoicesQueryHandler method 
            var data = await _context.Invoices
                         .ProjectTo<InvoiceDto>(_mapper.ConfigurationProvider)
                         .ToListAsync(cancellationToken);
            return data;
        }
    }
}

