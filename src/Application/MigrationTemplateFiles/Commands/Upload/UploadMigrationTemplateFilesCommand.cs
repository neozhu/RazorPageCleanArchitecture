
namespace CleanArchitecture.Razor.Application.MigrationTemplateFiles.Commands.Upload;

public class UploadMigrationTemplateFilesCommand : IRequest<Result>
{
    public string FileName { get; set; }
    public byte[] Data { get; set; }
}


public class UploadMigrationTemplateFilesCommandHandler :
             IRequestHandler<UploadMigrationTemplateFilesCommand, Result>
{
    private readonly IApplicationDbContext _context;
    private readonly IUploadService _uploadService;
    private readonly IMapper _mapper;
    private readonly IStringLocalizer<UploadMigrationTemplateFilesCommandHandler> _localizer;


    public UploadMigrationTemplateFilesCommandHandler(
        IApplicationDbContext context,
        IUploadService uploadService,
        IStringLocalizer<UploadMigrationTemplateFilesCommandHandler> localizer,
        IMapper mapper
        )
    {
        _context = context;
        _uploadService = uploadService;
        _localizer = localizer;
        _mapper = mapper;
    }
    public async Task<Result> Handle(UploadMigrationTemplateFilesCommand request, CancellationToken cancellationToken)
    {
        var result = await _uploadService.UploadAsync(new UploadRequest() { Data = request.Data, FileName = request.FileName, UploadType = UploadType.Document });
        var item = new MigrationTemplateFile()
        {
            FilePath = result,
            Name = request.FileName,
            Description=""
        };
        
        await  _context.MigrationTemplateFiles.AddAsync(item);
        await _context.SaveChangesAsync(cancellationToken);
        return Result.Success();
    }

}
