using CleanArchitecture.Razor.Application.Invoices.DTOs;

namespace CleanArchitecture.Razor.Application.Invoices.Queries.Export
{
    public class ExportInvoicesQuery : IRequest<byte[]>
    {
        public string FilterRules { get; set; }
        public string Sort { get; set; } = "Id";
        public string Order { get; set; } = "desc";
    }

    public class ExportInvoicesQueryHandler :
         IRequestHandler<ExportInvoicesQuery, byte[]>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly IExcelService _excelService;
        private readonly IStringLocalizer<ExportInvoicesQueryHandler> _localizer;

        public ExportInvoicesQueryHandler(
            IApplicationDbContext context,
            IMapper mapper,
            IExcelService excelService,
            IStringLocalizer<ExportInvoicesQueryHandler> localizer
            )
        {
            _context = context;
            _mapper = mapper;
            _excelService = excelService;
            _localizer = localizer;
        }

        public async Task<byte[]> Handle(ExportInvoicesQuery request, CancellationToken cancellationToken)
        {

            var filters = PredicateBuilder.FromFilter<Invoice>(request.FilterRules);
            var data = await _context.Invoices.Where(filters)
                       .OrderBy($"{request.Sort} {request.Order}")
                       .ProjectTo<InvoiceDto>(_mapper.ConfigurationProvider)
                       .ToListAsync(cancellationToken);
            var result = await _excelService.ExportAsync(data,
                new Dictionary<string, Func<InvoiceDto, object>>()
                {
                    { _localizer["Status"], item => item.Status },
                    { _localizer["Invoice No"], item => item.InvoiceNo },
                    { _localizer["Title"], item => item.Title },
                    { _localizer["Amount"], item => item.Amount },
                    { _localizer["Tax Rate"], item => item.TaxRate },
                    { _localizer["Tax"], item => item.Tax },
                    { _localizer["Invoice Date"], item => item.InvoiceDate },
                    { _localizer["Description"], item => item.Description },
                    { _localizer["Result"], item => item.Result },
                    { _localizer["AttachmentUrl"], item => item.AttachmentUrl }
                }
                , _localizer["Invoices"]);
            return result;
        }
    }
}

