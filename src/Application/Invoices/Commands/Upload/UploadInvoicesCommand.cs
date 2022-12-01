namespace CleanArchitecture.Razor.Application.Invoices.Commands.Upload
{
    public class UploadInvoicesCommand: IRequest<Result>
    {
        public string FileName { get; set; }
        public byte[] Data { get; set; }
    }


    public class UploadInvoicesCommandHandler : 
                 IRequestHandler<UploadInvoicesCommand, Result>
    {
        private readonly IApplicationDbContext _context;
        private readonly IUploadService _uploadService;
        private readonly IMapper _mapper;
        private readonly IStringLocalizer<UploadInvoicesCommandHandler> _localizer;


        public UploadInvoicesCommandHandler(
            IApplicationDbContext context,
            IUploadService uploadService,
            IStringLocalizer<UploadInvoicesCommandHandler> localizer,
            IMapper mapper
            )
        {
            _context = context;
            _uploadService = uploadService;
            _localizer = localizer;
            _mapper = mapper;
        }
        public async Task<Result> Handle(UploadInvoicesCommand request, CancellationToken cancellationToken)
        {
            var imgbase64string = Convert.ToBase64String(request.Data);
            var result =await _uploadService.UploadAsync(new UploadRequest() { Data = request.Data, FileName = request.FileName, UploadType= Domain.Enums.UploadType.Invoice });
            var invoice = new Invoice()
            {
                AttachmentUrl = result,
                Status = "Waiting",
                ImgString = imgbase64string
            };
            invoice.DomainEvents.Add(new InvoiceUploadedEvent(invoice));
            _context.Invoices.Add(invoice);
           await  _context.SaveChangesAsync(cancellationToken);
           return Result.Success();
        }
       
    }
}
