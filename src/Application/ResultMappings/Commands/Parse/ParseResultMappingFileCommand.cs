using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using CleanArchitecture.Razor.Application.ResultMappings.DTOs;

namespace CleanArchitecture.Razor.Application.ResultMappings.Commands.Parse;

public class ParseResultMappingFileCommand : IRequest<Result<ResultMappingDto>>
{
    public string FileName { get; set; }
    public byte[] Data { get; set; }
}
public class ParseResultMappingFileCommandHandler :
     IRequestHandler<ParseResultMappingFileCommand, Result<ResultMappingDto>>
{
    private readonly IApplicationDbContext _context;

    public ParseResultMappingFileCommandHandler(
         IApplicationDbContext context
        )
    {
        _context = context;
    }

    public async Task<Result<ResultMappingDto>> Handle(ParseResultMappingFileCommand request, CancellationToken cancellationToken)
    {

        var xmlstring = Encoding.UTF8.GetString(request.Data).Trim();
        var filename = request.FileName;
        try
        {
            var xdoc = XDocument.Parse(xmlstring);
            var resultmapping = new ResultMappingDto()
            {
                Name = filename.Remove(filename.LastIndexOf("."))
            };
            var data = xdoc.Descendants().Where(x => x.Name.LocalName == "Worksheet" && x.FirstAttribute.Value == "Data").First();
            var datatable = data.Descendants().Where(x => x.Name.LocalName == "Table");
            var description = "";
            var index = 0;
            var fieldlist = new List<FieldParameter>();
            foreach (var row in datatable.Descendants().Where(x => x.Name.LocalName == "Row").ToList())
            {
                if (index == 0)
                {
                    description = row.Descendants().Where(x => x.Name.LocalName == "Data").First().Value;
                    if (description != "")
                    {
                        resultmapping.Description = description;
                        resultmapping.TemplateDescription = description;
                    }
                }else if (index == 2) // find location
                {
                    var locations = row.Descendants().Where(x => x.Name.LocalName == "Data").Select(x => x.Value).ToArray();
                    if (locations.Length >= 2)
                    {
                        resultmapping.LegacySystem = locations[1];
                    }
                }else if (index == 3)
                {
                    var fields = row.Descendants().Where(x => x.Name.LocalName == "Data").Select(x => x.Value).ToArray();
                    foreach(var field in fields)
                    {
                        fieldlist.Add(new FieldParameter() { FieldName = field });
                    }
                }else if (index == 4)
                {
                    var descriptions = row.Descendants().Where(x => x.Name.LocalName == "Data").Select(x => x.Value).ToArray();
                    for(int i=0;i< descriptions.Length;i++)
                    {
                        var fieldpara = fieldlist[i];
                        fieldpara.Description = descriptions[i];
                    }
                    break;
                }

                resultmapping.FieldParameters = fieldlist;
                index++;
            }
            
        
            var exists = await _context.ResultMappings.AnyAsync(x =>
                      x.Name == resultmapping.Name &&
                      x.Description == resultmapping.Description
                      );
            if (exists)
            {
                return Result<ResultMappingDto>.Warning(resultmapping, new string[] { $"the {resultmapping.Name} already exists." });
            }
            else
            {
                return Result<ResultMappingDto>.Success(resultmapping);
            }

        }
        catch (Exception e)
        {
            return Result<ResultMappingDto>.Failure(new string[] { $"{filename} is not a valid SAP template file." });
        }

    }
}