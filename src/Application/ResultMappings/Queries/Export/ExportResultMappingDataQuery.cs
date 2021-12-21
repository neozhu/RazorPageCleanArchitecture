using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClosedXML.Excel;

namespace CleanArchitecture.Razor.Application.ResultMappings.Queries.Export;

public class ExportResultMappingDataQuery:IRequest<byte[]>
{
    public string FilterRules { get; set; }
    public string Sort { get; set; } = "Id";
    public string Order { get; set; } = "desc";
    public string Name { get; set; }
    public int Id { get; set; }
}

public class ExportResultMappingDataQueryHandler :
        IRequestHandler<ExportResultMappingDataQuery, byte[]>
{
    private readonly IApplicationDbContext _context;
    private readonly ICurrentUserService _userService;
    private readonly IMapper _mapper;

    public ExportResultMappingDataQueryHandler(
          IApplicationDbContext context,
          ICurrentUserService userService,
          IMapper mapper
        )
    {
        _context = context;
        _userService = userService;
        _mapper = mapper;
    }

    public async Task<byte[]> Handle(ExportResultMappingDataQuery request, CancellationToken cancellationToken)
    {
        var resultmapping = await _context.ResultMappings.FindAsync(request.Id);
        var filters = PredicateBuilder.FromFilter<ResultMappingData>(request.FilterRules);
        var data = await _context.ResultMappingDatas.Where(x=>x.ResultMappingId==request.Id)
                       .Where(filters)
                       .OrderBy($"{request.Sort} {request.Order}")
                       .ToListAsync(cancellationToken);

        using (var workbook = new XLWorkbook())
        {
            workbook.Properties.Author = _userService.UserId;
            var ws = workbook.Worksheets.Add("Data");
            var colIndex = 1;
            var rowIndex = 1;
            var headers = resultmapping.FieldParameters.Select(x=>x.FieldName).ToList();
            headers.Insert(0, "Status");
            headers.Add("Owner");
            headers.Add("Verified Date");
            var descriptions = resultmapping.FieldParameters.Select(x => x.Description).ToList();
            descriptions.Insert(0, "Verify Status");
            descriptions.Add("Verified User");
            descriptions.Add("Verified Date");
            foreach (var header in headers)
            {
                var cell = ws.Cell(rowIndex, colIndex);
                var fill = cell.Style.Fill;
                fill.PatternType = XLFillPatternValues.Solid;
                fill.SetBackgroundColor(XLColor.LightBlue);
                var border = cell.Style.Border;
                border.BottomBorder = XLBorderStyleValues.None;

                cell.Value = header;

                colIndex++;
            }
            colIndex = 1;
            rowIndex++;
            foreach (var description in descriptions)
            {
               
                var cell = ws.Cell(rowIndex, colIndex);
                var fill = cell.Style.Fill;
                fill.PatternType = XLFillPatternValues.Solid;
                fill.SetBackgroundColor(XLColor.LightGray);
                var border = cell.Style.Border;
                border.BottomBorder = XLBorderStyleValues.Thin;

                cell.Value = description;

                colIndex++;
            }
            var dataList = data.ToList();
            foreach (var item in dataList)
            {
                colIndex = 1;
                rowIndex++;
                ws.Cell(rowIndex, colIndex++).Value = item.Verify;
                foreach ((string key,string value) in item.FieldData)
                {
                    ws.Cell(rowIndex, colIndex++).Value = value;
                }
                ws.Cell(rowIndex, colIndex++).Value = item.Owner;
                ws.Cell(rowIndex, colIndex++).Value = item.VerifiedDate;
            }
            using (var stream = new MemoryStream())
            {
                workbook.SaveAs(stream);
                //var base64 = Convert.ToBase64String(stream.ToArray());
                stream.Seek(0, SeekOrigin.Begin);
                return await Task.FromResult(stream.ToArray());
            }
        }

    }
}
