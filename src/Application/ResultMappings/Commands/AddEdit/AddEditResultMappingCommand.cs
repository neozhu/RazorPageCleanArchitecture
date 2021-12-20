// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.


using System.Text;
using System.Xml.Linq;
using CleanArchitecture.Razor.Application.ResultMappings.DTOs;

namespace CleanArchitecture.Razor.Application.ResultMappings.Commands.AddEdit;

public class AddEditResultMappingCommand : ResultMappingDto, IRequest<Result<int>>, IMapFrom<ResultMapping>
{
    public UploadRequest UploadRequest { get; set; }
}

public class AddEditResultMappingCommandHandler : IRequestHandler<AddEditResultMappingCommand, Result<int>>
{
    private readonly IApplicationDbContext _context;
    private readonly IUploadService _uploadService;
    private readonly IMapper _mapper;
    private readonly IStringLocalizer<AddEditResultMappingCommandHandler> _localizer;
    public AddEditResultMappingCommandHandler(
        IApplicationDbContext context,
        IUploadService uploadService,
        IStringLocalizer<AddEditResultMappingCommandHandler> localizer,
        IMapper mapper
        )
    {
        _context = context;
        _uploadService = uploadService;
        _localizer = localizer;
        _mapper = mapper;
    }
    public async Task<Result<int>> Handle(AddEditResultMappingCommand request, CancellationToken cancellationToken)
    {
        var isexist = await isExist(request.Id,
           request.Name,
           request.LegacySystem,
           request.MigrationApproach,
           request.RelevantMock
        );
        if (isexist)
        {
            throw new Exception($"Duplicate result mapping name:{request.Name} ");
        }
        if (request.Id > 0)
        {
            var item = await _context.ResultMappings.FindAsync(new object[] { request.Id }, cancellationToken);
           item.Name= request.Name;
            item.Description= request.Description;
            //item.FieldParameters= request.FieldParameters;
            item.LegacySystem= request.LegacySystem;
            item.MigrationApproach= request.MigrationApproach;
            item.RelevantMock= request.RelevantMock;
            item.Team= request.Team;
            item.RelevantObjects= request.RelevantObjects;
            item.Status = "Ongoing";
           
            if (request.UploadRequest != null && request.UploadRequest.Data != null)
            {
                var result = await _uploadService.UploadAsync(request.UploadRequest);
                item.TemplateFile = result;
                var fieldparaters = readFieldParameter(request.UploadRequest.Data);
                var mappingdata = readResultMappingData(request.UploadRequest.Data);
                if (mappingdata.Count() > 0)
                {
                    var legacydata = await _context.ResultMappingDatas.Where(x => x.ResultMappingId == request.Id).ToListAsync();
                    _context.ResultMappingDatas.RemoveRange(legacydata);
                    foreach(var dataitem in mappingdata)
                    {
                        dataitem.ResultMappingId= request.Id;
                        await _context.ResultMappingDatas.AddAsync(dataitem, cancellationToken);
                    }
                    item.Verified = 0;
                    item.Total = mappingdata.Count();
                }
                item.FieldParameters = fieldparaters.ToList();
                //item.ResultMappingDatas = mappingdata.ToList();
            }
            _context.ResultMappings.Update(item);

            await _context.SaveChangesAsync(cancellationToken);
            return Result<int>.Success(item.Id);
        }
        else
        {
            var item = _mapper.Map<ResultMapping>(request);
            if (request.UploadRequest != null && request.UploadRequest.Data != null)
            {
                var result = await _uploadService.UploadAsync(request.UploadRequest);
                item.TemplateFile = result;
                var fieldparaters = readFieldParameter(request.UploadRequest.Data);
                var mappingdata = readResultMappingData(request.UploadRequest.Data);
                item.FieldParameters = fieldparaters.ToList();
                item.ResultMappingDatas = mappingdata.ToList();
                item.Verified = 0;
                item.Total = 0;
            }
            _context.ResultMappings.Add(item);
            await _context.SaveChangesAsync(cancellationToken);
            return Result<int>.Success(item.Id);
        }

    }

    private async Task<bool> isExist(int id, string name,string location,string migrationApproach,string relevantMock)
    {
        return await _context.ResultMappings.AnyAsync(x => x.Id != id &&
        x.Name==name &&
        x.RelevantMock == relevantMock &&
        x.LegacySystem == location &&
        x.MigrationApproach == migrationApproach);
    }

    private IEnumerable<FieldParameter> readFieldParameter(byte[] filedata)
    {
        var xmlstring = Encoding.UTF8.GetString(filedata).Trim();
        var xdoc = XDocument.Parse(xmlstring);
        var data = xdoc.Descendants().Where(x => x.Name.LocalName == "Worksheet" && x.FirstAttribute.Value == "Data").First();
        var datatable = data.Descendants().Where(x => x.Name.LocalName == "Table");
        var index = 0;
        var fieldlist = new List<FieldParameter>();
        foreach (var row in datatable.Descendants().Where(x => x.Name.LocalName == "Row").ToList())
        {
           if (index == 3)
            {
                var fields = row.Descendants().Where(x => x.Name.LocalName == "Data").Select(x => x.Value).ToArray();
                foreach (var field in fields)
                {
                    fieldlist.Add(new FieldParameter() { FieldName = field });
                }
            }
            else if (index == 4)
            {
                var descriptions = row.Descendants().Where(x => x.Name.LocalName == "Data").Select(x => x.Value).ToArray();
                for (int i = 0; i < descriptions.Length; i++)
                {
                    var fieldpara = fieldlist[i];
                    fieldpara.Description = descriptions[i];
                }
                break;
            }
            index++;
        }
        return fieldlist;
    }
    private IEnumerable<ResultMappingData> readResultMappingData(byte[] filedata)
    {
        var xmlstring = Encoding.UTF8.GetString(filedata).Trim();
        var xdoc = XDocument.Parse(xmlstring);
        var data = xdoc.Descendants().Where(x => x.Name.LocalName == "Worksheet" && x.FirstAttribute.Value == "Data").First();
        var datatable = data.Descendants().Where(x => x.Name.LocalName == "Table");
        var index = 0;
        var datalist = new List<ResultMappingData>();
        var fieldarray =new List<string>();
        foreach (var row in datatable.Descendants().Where(x => x.Name.LocalName == "Row").Skip(3).ToList())
        {
           
            var fielddata=new Dictionary<string, string>();

            if (index == 0)
            {
                var fields = row.Descendants().Where(x => x.Name.LocalName == "Data").Select(x => x.Value).ToArray();
                foreach (var field in fields)
                {
                    fieldarray.Add(field);
                }
            }
            else if (index >= 2)
            {
                var mappingdata = new ResultMappingData()
                {

                };
                var mappingdataType = typeof(ResultMappingData);
                var dataarray = row.Descendants().Where(x => x.Name.LocalName == "Data").Select(x => x.Value).ToArray();
                for (int i = 0; i < dataarray.Length; i++)
                {
                    var fieldName = fieldarray[i];
                    fielddata.Add(fieldName, dataarray[i]);
                    var propertyName = "Field" + (i + 1).ToString();
                    var propertyInfo = mappingdataType.GetProperty(propertyName);
                    if (propertyInfo != null)
                    {
                        propertyInfo.SetValue(mappingdata, dataarray[i], null);
                    }
                   
                }
                mappingdata.FieldData = fielddata;
                datalist.Add(mappingdata);

            }
            index++;
           
        }
        return datalist;
    }
}

