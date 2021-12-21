// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using CleanArchitecture.Razor.Application.ResultMappings.DTOs;

namespace CleanArchitecture.Razor.Application.ResultMappings.Queries.GetAll;

public class GetAllResultMappingsQuery : IRequest<IEnumerable<ResultMappingDto>>
{

}

public class SummarizingByStatusQuery : IRequest<IEnumerable<StatusSummarizingDto>>
{

}

public class SummarizingVerifiedByIdQuery : IRequest<Dictionary<string,int[]>>
{
    public int Id { get; set; }
}
public class GetOwnerListByIdQuery : IRequest<IEnumerable<object>>
{
    public int Id { get; set; }
}

public class GetAllResultMappingsQueryHandler :
     IRequestHandler<GetOwnerListByIdQuery, IEnumerable<object>>,
     IRequestHandler<SummarizingVerifiedByIdQuery, Dictionary<string, int[]>>,
     IRequestHandler<SummarizingByStatusQuery, IEnumerable<StatusSummarizingDto>>,
     IRequestHandler<GetAllResultMappingsQuery, IEnumerable<ResultMappingDto>>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;
    private readonly IStringLocalizer<GetAllResultMappingsQueryHandler> _localizer;

    public GetAllResultMappingsQueryHandler(
        IApplicationDbContext context,
        IMapper mapper,
        IStringLocalizer<GetAllResultMappingsQueryHandler> localizer
        )
    {
        _context = context;
        _mapper = mapper;
        _localizer = localizer;
    }

    public async Task<IEnumerable<ResultMappingDto>> Handle(GetAllResultMappingsQuery request, CancellationToken cancellationToken)
    {

        var data = await _context.ResultMappings
                     .ProjectTo<ResultMappingDto>(_mapper.ConfigurationProvider)
                     .ToListAsync(cancellationToken);
        return data;
    }

    public async Task<IEnumerable<StatusSummarizingDto>> Handle(SummarizingByStatusQuery request, CancellationToken cancellationToken)
    {
        var count = await _context.ResultMappings.CountAsync();
        if (count > 0)
        {
            var status = new string[] { "Not started", "Ongoing", "Finished" };
            var result = await _context.ResultMappings.GroupBy(x => x.Status)
                      .Select(x => new StatusSummarizingDto { Status = x.Key, Total = x.Count(), Percentage =x.Count() * 100 / count  })
                      .ToListAsync();
            foreach(var item in status)
            {
                if (!result.Any(x => x.Status == item))
                {
                    result.Add(new StatusSummarizingDto() { Status = item });
                }
            }
            return result;
        }
        return new List<StatusSummarizingDto>() {
            new StatusSummarizingDto(){Status="Not started" },
            new StatusSummarizingDto(){Status="Ongoing" },
            new StatusSummarizingDto(){Status="Finished" },
            };
    }

    public async Task<Dictionary<string, int[]>> Handle(SummarizingVerifiedByIdQuery request, CancellationToken cancellationToken)
    {
        var total = await _context.ResultMappingDatas
            .Where(x=>x.ResultMappingId==request.Id && x.Owner!=null && (x.Verify=="Scoped" || x.Verify == "Verified"))
            .GroupBy(x=>x.Owner)
            .Select(x => new {Owner =x.Key, Total=x.Count() })
            .ToListAsync();
        var verified = await _context.ResultMappingDatas
            .Where(x => x.ResultMappingId == request.Id && x.Owner != null && x.Verify == "Verified")
            .GroupBy(x => x.Owner)
            .Select(x => new { Owner = x.Key, Verified = x.Count() })
            .ToListAsync();
        var result = new Dictionary<string, int[]>();
        foreach(var item in total)
        {
            var verifiedcount = verified.Where(x => x.Owner == item.Owner).FirstOrDefault()?.Verified??0;
            var percentage =(int)Math.Round(verifiedcount * 100m / item.Total);
            result.Add(item.Owner, new int[] { percentage, verifiedcount, item.Total });
        }

        return result;

    }

    public async Task<IEnumerable<object>> Handle(GetOwnerListByIdQuery request, CancellationToken cancellationToken)
    {
        var result = await _context.ResultMappingDatas.
                         Where(x => x.Owner != null && x.ResultMappingId==request.Id)
                         .Select(x=>new {text = x.Owner }).Distinct().ToListAsync();
        return result.ToArray();
    }
}


