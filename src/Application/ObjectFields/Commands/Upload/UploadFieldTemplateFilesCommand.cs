using System.Xml.Linq;
namespace CleanArchitecture.Razor.Application.ObjectFields.Commands.Upload;

public class UploadFieldTemplateFilesCommand : IRequest<Result>
{
    public string FileName { get; set; }
    public byte[] Data { get; set; }
}


public class UploadFieldTemplateFilesCommandHandler :
             IRequestHandler<UploadFieldTemplateFilesCommand, Result>
{
    private readonly IApplicationDbContext _context;
    private readonly IUploadService _uploadService;
    private readonly IMapper _mapper;
    private readonly IStringLocalizer<UploadFieldTemplateFilesCommandHandler> _localizer;


    public UploadFieldTemplateFilesCommandHandler(
        IApplicationDbContext context,
        IUploadService uploadService,
        IStringLocalizer<UploadFieldTemplateFilesCommandHandler> localizer,
        IMapper mapper
        )
    {
        _context = context;
        _uploadService = uploadService;
        _localizer = localizer;
        _mapper = mapper;
    }
    public async Task<Result> Handle(UploadFieldTemplateFilesCommand request, CancellationToken cancellationToken)
    {
        //var result = await _uploadService.UploadAsync(new UploadRequest() { Data = request.Data, FileName = request.FileName, UploadType = UploadType.Document });
        var text = System.Text.Encoding.UTF8.GetString(request.Data).Trim();
        var xdoc = XDocument.Parse(text);
        var signature = xdoc.Descendants().Where(x => x.Name.LocalName == "Worksheet" && x.FirstAttribute.Value == "Signature").First();
        var signaturetable = signature.Descendants().Where(x => x.Name.LocalName == "Table");
        var data = xdoc.Descendants().Where(x => x.Name.LocalName == "Worksheet" && x.FirstAttribute.Value == "Data").First();
        var datatable = data.Descendants().Where(x => x.Name.LocalName == "Table");
        var description = "";
        foreach (var row in datatable.Descendants().Where(x => x.Name.LocalName == "Row").ToList())
        {
            description = row.Descendants().Where(x => x.Name.LocalName == "Data").First().Value;
            if (description != "")
            {
                break;
            }
        }
        foreach (var row in signaturetable.Descendants().Where(x => x.Name.LocalName == "Row" && x.Nodes().Count() == 7).Skip(1).ToList())
        {
            var cells = row.Nodes().ToArray();
            var paraname = ((XElement)cells[0]).Elements().Where(x => x.Name.LocalName == "Data").First().Value;
            var direct = ((XElement)cells[1]).Elements().Where(x=>x.Name.LocalName== "Data").First().Value;
            var associatedtype = ((XElement)cells[3]).Elements().Where(x => x.Name.LocalName == "Data").First().Value;
            var datatype = ((XElement)cells[4]).Elements().Where(x => x.Name.LocalName == "Data").First().Value;
            var length = ((XElement)cells[5]).Elements().Where(x => x.Name.LocalName == "Data").First().Value;
            var name = paraname;
            if (paraname.IndexOf("_") > 0)
            {
                name = paraname.Split('_')[1];
            }
            var item = new ObjectField()
            {
                ParameterName = paraname,
                Direct = direct,
                AssociatedType = associatedtype,
                Length =string.IsNullOrEmpty(length)?0 :Convert.ToInt32(length),
                SourceTemplateName =request.FileName,
                Title = description,
                Name= name
            };

            await _context.ObjectFields.AddAsync(item);
        }


       
        await _context.SaveChangesAsync(cancellationToken);
        return Result.Success();
    }

}
