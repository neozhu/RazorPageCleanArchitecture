using System.Text;
using System.Xml.Linq;
using CleanArchitecture.Razor.Application.MappingRules.DTOs;

namespace CleanArchitecture.Razor.Application.MappingRules.Commands.Parse;

public class ParseTemplateFileCommand : IRequest<Result<MappingRuleDto>>
{
    public string FileName { get; set; }
    public byte[] Data { get; set; }
}


public class ParseTemplateFileCommandHandler:
     IRequestHandler<ParseTemplateFileCommand, Result<MappingRuleDto>>{
    private readonly IApplicationDbContext _context;

    public ParseTemplateFileCommandHandler(
         IApplicationDbContext context
        )
    {
        _context = context;
    }

    public async Task<Result<MappingRuleDto>> Handle(ParseTemplateFileCommand request, CancellationToken cancellationToken)
    {

        var xmlstring = Encoding.UTF8.GetString(request.Data).Trim();
        var filename = request.FileName;
        try
        {
            var xdoc = XDocument.Parse(xmlstring);
            var mappingruledto = new MappingRuleDto() {
                Name = filename.Remove(filename.LastIndexOf("."))
            };
            var objectname= xdoc.Descendants().Where(x => x.Name.LocalName == "Object_name").First().Value;
            mappingruledto.ObjectName = objectname;
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
                    mappingruledto.TemplateDescription = description;
                    break;
                }
            }
            int index = 0;
            foreach (var row in signaturetable.Descendants().Where(x => x.Name.LocalName == "Row" && x.Nodes().Count() == 7).Skip(1).ToList())
            {
               
                var cells = row.Nodes().ToArray();
                var paraname = ((XElement)cells[0]).Elements().Where(x => x.Name.LocalName == "Data").First().Value;
                var direct = ((XElement)cells[1]).Elements().Where(x => x.Name.LocalName == "Data").First().Value;
                var associatedtype = ((XElement)cells[3]).Elements().Where(x => x.Name.LocalName == "Data").First().Value;
                var datatype = ((XElement)cells[4]).Elements().Where(x => x.Name.LocalName == "Data").First().Value;
                var length = ((XElement)cells[5]).Elements().Where(x => x.Name.LocalName == "Data").First().Value;
                var name = paraname;
               
                if (paraname.IndexOf("_") > 0)
                {
                    name = paraname.Substring(paraname.IndexOf("_") +1);
                }
                var fielddescription = await _context.ObjectFields.FirstOrDefaultAsync(x => x.Name == name);
                if (direct == "Import Parameter")
                {
                    index++;
                    switch (index)
                    {
                        case 1:
                            mappingruledto.LegacyField1 = name;
                            mappingruledto.ImportParameterField1 = paraname;
                            mappingruledto.LegacyDescription1 = fielddescription?.Description;
                            break;
                        case 2:
                            mappingruledto.LegacyField2 = name;
                            mappingruledto.ImportParameterField2 = paraname;
                            mappingruledto.LegacyDescription2 = fielddescription?.Description;
                            break;
                        case 3:
                            mappingruledto.LegacyField3 = name;
                            mappingruledto.ImportParameterField3 = paraname;
                            mappingruledto.LegacyDescription3 = fielddescription?.Description;
                            break;
                        default:
                            mappingruledto.LegacyField4 = name;
                            mappingruledto.ImportParameterField4 = paraname;
                            mappingruledto.LegacyDescription4 = fielddescription?.Description;
                            break;
                    }
                }else if(direct == "Export Parameter")
                {
                    mappingruledto.NewValueField = name;
                    mappingruledto.ExportParameterField = paraname;
                    mappingruledto.NewValueFieldDescription= fielddescription?.Description;
                }
            }
            var exists = await _context.MappingRules.AnyAsync(x=>
                      x.LegacyField1== mappingruledto.LegacyField1
                      && x.LegacyField2 == mappingruledto.LegacyField2
                      && x.LegacyField3 == mappingruledto.LegacyField3
                      && x.NewValueField == mappingruledto.NewValueField
                      && x.LegacySystem == mappingruledto.LegacySystem
                      );
            if (exists)
            {
                return Result<MappingRuleDto>.Warning(mappingruledto, new string[] { $"the {mappingruledto.Name} already exists." });
            }
            else
            {
                return Result<MappingRuleDto>.Success(mappingruledto);
            }
           
        }
        catch(Exception e)
        {
            return Result<MappingRuleDto>.Failure(new string[] { $"{filename} is not a valid SAP template file." });
        }
            
        }
}