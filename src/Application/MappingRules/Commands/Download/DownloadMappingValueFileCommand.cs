using System.Text;
using System.Text.RegularExpressions;
using System.Xml.Linq;
using Ionic.Zip;

namespace CleanArchitecture.Razor.Application.MappingRules.Commands.Download;

public class DownloadMappingValueFileCommand : IRequest<byte[]>
{
    public int MappingRuleId { get; set; }
}

public class DownloadZipArchiveMappingValueFileCommand : IRequest<byte[]>
{
    public int[] MappingRuleId { get; set; }
}

public class DownloadMappingValueFileCommandHandler :
     IRequestHandler<DownloadZipArchiveMappingValueFileCommand, byte[]>,
     IRequestHandler<DownloadMappingValueFileCommand, byte[]>
{
    private readonly IApplicationDbContext _context;
    private readonly ILogger<DownloadMappingValueFileCommandHandler> _logger;

    public DownloadMappingValueFileCommandHandler(
        IApplicationDbContext context,
        ILogger<DownloadMappingValueFileCommandHandler> logger
        )
    {
        _context = context;
        _logger = logger;
    }

    public async Task<byte[]> Handle(DownloadMappingValueFileCommand request, CancellationToken cancellationToken)
    {
        (var buffer, var filename)= await GenerateXml(request.MappingRuleId);
        return buffer;
    }

    private async Task<(byte[],string)> GenerateXml(int id)
    {
        var mappingrule = await _context.MappingRules.FirstAsync(x => x.Id == id);
        var fi = new FileInfo(mappingrule.TemplateFile);
        var fileName = fi.Name;
        var values = await _context.FieldMappingValues.Where(x => x.MappingRuleId == id).ToListAsync();
        var path = Path.Combine(Directory.GetCurrentDirectory(), mappingrule.TemplateFile);
        if (!File.Exists(path))
        {
            throw new Exception($"Not found value mapping template file:{mappingrule.TemplateFile}");
        }
        var buffer = File.ReadAllBytes(path);
        if (values.Count > 0)
        {
            var xmlstr = Encoding.UTF8.GetString(buffer).Trim();
            var xdoc = XDocument.Parse(xmlstr, LoadOptions.PreserveWhitespace);
            var namespaces = xdoc.Root.Attributes().
                             Where(a => a.IsNamespaceDeclaration).
                                 GroupBy(a => a.Name.Namespace == XNamespace.None ? String.Empty : a.Name.LocalName,
                                     a => XNamespace.Get(a.Value)).
                                       ToDictionary(g => g.Key,
                                           g => g.First());
            var worksheet = xdoc.Descendants().Where(x => x.Name.LocalName == "Worksheet" && x.FirstAttribute.Value == "Data").First();
            var table = worksheet.Descendants().Where(x => x.Name.LocalName == "Table").First();
            var expandedRowCount = table.Attributes().Where(x => x.Name.LocalName == "ExpandedRowCount").First();
            var rowCount = int.Parse(expandedRowCount.Value);
            var header = table.Descendants().Where(x => x.Name.LocalName == "Row").Skip(3).First();
            var fields = header.Descendants().Where(x => x.Name.LocalName == "Cell").SelectMany(x => x.Elements().Where(x => x.Name.LocalName == "Data"))
                .Select(x => x.Value).ToList();
            var valueCount = values.Count;
            var type = header.Descendants().Where(x => x.Name.LocalName == "Data").SelectMany(x => x.Attributes().Where(y => y.Name.LocalName == "Type")).First().Value;
            switch (fields.Count)
            {
                case 2:
                    foreach (var item in values)
                    {
                        table.Add(new XElement(namespaces.First().Value + "Row",
                           new XAttribute(namespaces["ss"] + "AutoFitHeight", 0),
                               "\r\n",
                               new XElement(namespaces.First().Value + "Cell",
                                   new XElement(namespaces.First().Value + "Data", new XAttribute(namespaces["ss"] + "Type", type), item.Legacy1)
                               ),
                              "\r\n",
                               new XElement(namespaces.First().Value + "Cell",
                                   new XElement(namespaces.First().Value + "Data", new XAttribute(namespaces["ss"] + "Type", type), item.NewValue)
                               ),
                               "\r\n"
                          ), "\r\n");
                    }
                    break;
                case 3:
                    foreach (var item in values)
                    {
                        table.Add(new XElement(namespaces.First().Value + "Row",
                           new XAttribute(namespaces["ss"] + "AutoFitHeight", 0),
                              "\r\n",
                               new XElement(namespaces.First().Value + "Cell",
                                   new XElement(namespaces.First().Value + "Data", new XAttribute(namespaces["ss"] + "Type", type), item.Legacy1)
                               ),
                               "\r\n",
                               new XElement(namespaces.First().Value + "Cell",
                                   new XElement(namespaces.First().Value + "Data", new XAttribute(namespaces["ss"] + "Type", type), item.Legacy2)
                               ),
                               "\r\n",
                              new XElement(namespaces.First().Value + "Cell",
                                   new XElement(namespaces.First().Value + "Data", new XAttribute(namespaces["ss"] + "Type", type), item.NewValue)
                               ),
                              "\r\n"
                          ), "\r\n");
                    }

                    break;
                case 4:
                    foreach (var item in values)
                    {
                        table.Add(new XElement(namespaces.First().Value + "Row",
                           new XAttribute(namespaces["ss"] + "AutoFitHeight", 0),
                               "\r\n",
                               new XElement(namespaces.First().Value + "Cell",
                                   new XElement(namespaces.First().Value + "Data", new XAttribute(namespaces["ss"] + "Type", type), item.Legacy1)
                               ),
                               "\r\n",
                               new XElement(namespaces.First().Value + "Cell",
                                   new XElement(namespaces.First().Value + "Data", new XAttribute(namespaces["ss"] + "Type", type), item.Legacy2)
                               ),
                              "\r\n",
                              new XElement(namespaces.First().Value + "Cell",
                                   new XElement(namespaces.First().Value + "Data", new XAttribute(namespaces["ss"] + "Type", type), item.Legacy3)
                               ),
                               "\r\n",
                               new XElement(namespaces.First().Value + "Cell",
                                   new XElement(namespaces.First().Value + "Data", new XAttribute(namespaces["ss"] + "Type", type), item.NewValue)
                               ),
                               "\r\n"
                          ), "\r\n");
                    }

                    break;
            }
            expandedRowCount.Value = (rowCount + valueCount).ToString();
            using (TextWriter writer = new Utf8StringWriter())
            {
                xdoc.Save(writer, SaveOptions.DisableFormatting);
                var output = writer.ToString();
                // replace default namespace prefix:<ss:
                var result = Regex.Replace(Regex.Replace(output, "<ss:", "<"), "</ss:", "</");
                _logger.LogInformation("Download the value mapping file:{@Request}", fileName);
                return (Encoding.UTF8.GetBytes(result),fileName);
            }
        }
        else
        {
            return (buffer, fileName);
        }
    }

    public async Task<byte[]> Handle(DownloadZipArchiveMappingValueFileCommand request, CancellationToken cancellationToken)
    {
        using (var zip = new ZipFile())
        {
            zip.AlternateEncodingUsage = ZipOption.AsNecessary;
            foreach(var id in request.MappingRuleId)
            {
                (var buffer, var filename) = await GenerateXml(id);
                zip.AddEntry(filename, buffer);
            }
            
            
            using (MemoryStream memoryStream = new MemoryStream())
            {
                zip.Save(memoryStream);
                return memoryStream.ToArray();
            }
        }
    }

    public class Utf8StringWriter : StringWriter
    {
        public override Encoding Encoding { get { return Encoding.UTF8; } }
    }
}
