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

public class SummarizingVerifiedByIdQuery : IRequest<int[]>
{
    public int Id { get; set; }
}

public class GetAllResultMappingsQueryHandler :
     IRequestHandler<SummarizingVerifiedByIdQuery, int[]>,
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

    public async Task<int[]> Handle(SummarizingVerifiedByIdQuery request, CancellationToken cancellationToken)
    {
        var verified = await _context.ResultMappingDatas.CountAsync(x=>x.Verify == "Verified" && x.ResultMappingId==request.Id);
        var total = await _context.ResultMappingDatas.CountAsync(x=>x.ResultMappingId == request.Id);
        if (total > 0)
        {
            return new int[] { ((int)Math.Round( Convert.ToDecimal(verified) * 100.00m / Convert.ToDecimal(total))),verified, total };
        }
        else
        {
            return new int[] { 0, 0, 0 };
        }

    }
}


