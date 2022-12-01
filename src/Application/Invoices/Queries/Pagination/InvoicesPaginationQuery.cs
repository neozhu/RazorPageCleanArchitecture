using CleanArchitecture.Razor.Application.Invoices.DTOs;

namespace CleanArchitecture.Razor.Application.Invoices.Queries.Pagination
{
    public class InvoicesWithPaginationQuery : PaginationRequest, IRequest<PaginatedData<InvoiceDto>>
    {
       
    }
    
    public class InvoicesWithPaginationQueryHandler :
         IRequestHandler<InvoicesWithPaginationQuery, PaginatedData<InvoiceDto>>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly IStringLocalizer<InvoicesWithPaginationQueryHandler> _localizer;

        public InvoicesWithPaginationQueryHandler(
            IApplicationDbContext context,
            IMapper mapper,
            IStringLocalizer<InvoicesWithPaginationQueryHandler> localizer
            )
        {
            _context = context;
            _mapper = mapper;
            _localizer = localizer;
        }

        public async Task<PaginatedData<InvoiceDto>> Handle(InvoicesWithPaginationQuery request, CancellationToken cancellationToken)
        {
           var filters = PredicateBuilder.FromFilter<Invoice>(request.FilterRules);
           var data = await _context.Invoices.Where(filters)
                .OrderBy($"{request.Sort} {request.Order}")
                .ProjectTo<InvoiceDto>(_mapper.ConfigurationProvider)
                .PaginatedDataAsync(request.Page, request.Rows);
            return data;
        }
    }
}

