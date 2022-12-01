using CleanArchitecture.Razor.Application.InvoiceRawDatas.DTOs;

namespace CleanArchitecture.Razor.Application.Invoices.Queries.GetRawData
{
    public class GetInvoiceRawDatasQuery : IRequest<IEnumerable<InvoiceRawDataDto>>
    {
       public int InvoiceId { get; set; }
    }
    
    public class GetInvoiceRawDatasQueryHandler :
         IRequestHandler<GetInvoiceRawDatasQuery, IEnumerable<InvoiceRawDataDto>>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly IStringLocalizer<GetInvoiceRawDatasQueryHandler> _localizer;

        public GetInvoiceRawDatasQueryHandler(
            IApplicationDbContext context,
            IMapper mapper,
            IStringLocalizer<GetInvoiceRawDatasQueryHandler> localizer
            )
        {
            _context = context;
            _mapper = mapper;
            _localizer = localizer;
        }

        public async Task<IEnumerable<InvoiceRawDataDto>> Handle(GetInvoiceRawDatasQuery request, CancellationToken cancellationToken)
        {
            var data = await _context.InvoiceRawDatas.Where(x=>x.InvoiceId==request.InvoiceId)
                         .ProjectTo<InvoiceRawDataDto>(_mapper.ConfigurationProvider)
                         .ToListAsync(cancellationToken);
            return data;
        }
    }
}

