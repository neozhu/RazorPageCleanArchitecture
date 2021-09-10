using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using CleanArchitecture.Razor.Application.Common.Interfaces;
using CleanArchitecture.Razor.Application.Common.Mappings;
using CleanArchitecture.Razor.Application.Common.Models;
using CleanArchitecture.Razor.Application.SalesContractDetails.DTOs;
using CleanArchitecture.Razor.Domain.Entities;
using CleanArchitecture.Razor.Domain.Events;
using MediatR;
using Microsoft.Extensions.Localization;

namespace CleanArchitecture.Razor.Application.SalesContractDetails.Commands.Create
{
    public class CreateSalesContractDetailCommand: SalesContractDetailDto,IRequest<Result>, IMapFrom<SalesContractDetail>
    {
        public void Mapping(Profile profile)
        {
            profile.CreateMap<SalesContractDetail, CreateSalesContractDetailCommand>().ReverseMap();
        }
    }
    

    public class CreateSalesContractDetailCommandHandler : IRequestHandler<CreateSalesContractDetailCommand, Result>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly IStringLocalizer<CreateSalesContractDetailCommand> _localizer;
        public CreateSalesContractDetailCommandHandler(
            IApplicationDbContext context,
            IStringLocalizer<CreateSalesContractDetailCommand> localizer,
            IMapper mapper
            )
        {
            _context = context;
            _localizer = localizer;
            _mapper = mapper;
        }
        public async Task<Result> Handle(CreateSalesContractDetailCommand request, CancellationToken cancellationToken)
        {
       
           var item = _mapper.Map<SalesContractDetail>(request);
            _context.SalesContractDetails.Add(item);
            await _context.SaveChangesAsync(cancellationToken);
            return  Result.Success();
        }
    }
}
