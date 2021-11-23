
using System.Xml.Linq;
using ClosedXML.Excel;
using DocumentFormat.OpenXml.Packaging;

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
        var xdoc = XDocument.Load(new MemoryStream(request.Data));
        var signature = xdoc.Descendants().Where(x => x.Name.LocalName == "Worksheet" && x.FirstAttribute.Value == "Signature").First();
        var table = signature.Descendants().Where(x => x.Name.LocalName == "Table");
        var index = 0;
        foreach (var row in table.Descendants().Where(x => x.Name.LocalName == "Row" && x.Nodes().Count() == 7).Skip(1).ToList())
        {
            var cells = row.Nodes().ToArray();
            var paraname = ((XElement)cells[0]).Elements().Where(x => x.Name.LocalName == "Data").First().Value;
            var direct = ((XElement)cells[1]).Elements().Where(x=>x.Name.LocalName== "Data").First().Value;
            if (direct == "Import Parameter")
            {
                index++;
                switch (index)
                {
                    case 0:
                        item.Legacy1Field = paraname;
                        break;
                    case 1:
                        item.Legacy2Field = paraname;
                        break;
                    case 2:
                        item.Legacy3Field = paraname;
                        break;
                }
            }
            else
            {
                item.NewValueField = paraname;
            }
            
            

        }

        await  _context.MigrationTemplateFiles.AddAsync(item);
        await _context.SaveChangesAsync(cancellationToken);
        return Result.Success();
    }

}
