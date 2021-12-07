using System.Text;
using System.Text.RegularExpressions;
using System.Xml.Linq;

namespace CleanArchitecture.Razor.Application.MappingRules.Commands.Download;

public class DownloadMappingValueFileCommand : IRequest<byte[]>
{
    public int MappingRuleId { get; set; }
}

public class DownloadMappingValueFileCommandHandler :
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
        var mappingrule = await _context.MappingRules.FirstAsync(x => x.Id == request.MappingRuleId);
        var values = await _context.FieldMappingValues.Where(x => x.MappingRuleId == request.MappingRuleId).ToListAsync();
        var path = Path.Combine(Directory.GetCurrentDirectory(), mappingrule.TemplateFile);
        if (!File.Exists(path))
        {
            throw new Exception($"Not found mapping value template file:{mappingrule.TemplateFile}");
        }
        var buffer = File.ReadAllBytes(path);
        if (values.Count > 0)
        {
            var xmlstr = Encoding.UTF8.GetString(buffer).Trim();
            var xdoc = XDocument.Parse(xmlstr);
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
                               new XElement(namespaces.First().Value + "Cell",
                                   new XElement(namespaces.First().Value + "Data", new XAttribute(namespaces["ss"] + "Type", type), item.Legacy1)
                               ),
                               new XElement(namespaces.First().Value + "Cell",
                               new XElement(namespaces.First().Value + "Data", new XAttribute(namespaces["ss"] + "Type", type), item.NewValue)
                               )
                          ));
                    }
                    break;
                case 3:
                    foreach (var item in values)
                    {
                        table.Add(new XElement(namespaces.First().Value + "Row",
                           new XAttribute(namespaces["ss"] + "AutoFitHeight", 0),
                               new XElement(namespaces.First().Value + "Cell",
                                   new XElement(namespaces.First().Value + "Data", new XAttribute(namespaces["ss"] + "Type", type), item.Legacy1)
                               ),
                               new XElement(namespaces.First().Value + "Cell",
                                   new XElement(namespaces.First().Value + "Data", new XAttribute(namespaces["ss"] + "Type", type), item.Legacy2)
                               ),
                              new XElement(namespaces.First().Value + "Cell",
                               new XElement(namespaces.First().Value + "Data", new XAttribute(namespaces["ss"] + "Type", type), item.NewValue)
                               )
                          ));
                    }

                    break;
                case 4:
                    foreach (var item in values)
                    {
                        table.Add(new XElement(namespaces.First().Value + "Row",
                           new XAttribute(namespaces["ss"] + "AutoFitHeight", 0),
                               new XElement(namespaces.First().Value + "Cell",
                                   new XElement(namespaces.First().Value + "Data", new XAttribute(namespaces["ss"] + "Type", type), item.Legacy1)
                               ),
                               new XElement(namespaces.First().Value + "Cell",
                                   new XElement(namespaces.First().Value + "Data", new XAttribute(namespaces["ss"] + "Type", type), item.Legacy2)
                               ),
                              new XElement(namespaces.First().Value + "Cell",
                                   new XElement(namespaces.First().Value + "Data", new XAttribute(namespaces["ss"] + "Type", type), item.Legacy3)
                               ),
                               new XElement(namespaces.First().Value + "Cell",
                                   new XElement(namespaces.First().Value + "Data", new XAttribute(namespaces["ss"] + "Type", type), item.NewValue)
                               )
                          ));
                    }

                    break;
            }
            expandedRowCount.Value = (rowCount + valueCount).ToString();
            using (TextWriter writer = new StringWriter())
            {
                xdoc.Save(writer);
                var output = writer.ToString();
                // replace default namespace prefix:<ss:
                string result = Regex.Replace(Regex.Replace(output, "<ss:", "<"), "</ss:", "</");
                return Encoding.UTF8.GetBytes(result);
            }
        }
        else
        {
            return buffer;
        }

    }
}
