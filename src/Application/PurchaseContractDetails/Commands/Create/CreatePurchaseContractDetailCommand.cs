using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using CleanArchitecture.Razor.Application.Common.Interfaces;
using CleanArchitecture.Razor.Application.Common.Mappings;
using CleanArchitecture.Razor.Application.Common.Models;
using CleanArchitecture.Razor.Application.PurchaseContractDetails.DTOs;
using CleanArchitecture.Razor.Domain.Entities;
using CleanArchitecture.Razor.Domain.Events;
using MediatR;
using Microsoft.Extensions.Localization;

namespace CleanArchitecture.Razor.Application.PurchaseContractDetails.Commands.Create
{
    public class CreatePurchaseContractDetailCommand: PurchaseContractDetailDto,IRequest<Result>, IMapFrom<PurchaseContractDetail>
    {
        public void Mapping(Profile profile)
        {
            profile.CreateMap<PurchaseContractDetail, CreatePurchaseContractDetailCommand>().ReverseMap();
        }
    }
    

    public class CreatePurchaseContractDetailCommandHandler : IRequestHandler<CreatePurchaseContractDetailCommand, Result>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly IStringLocalizer<CreatePurchaseContractDetailCommand> _localizer;
        public CreatePurchaseContractDetailCommandHandler(
            IApplicationDbContext context,
            IStringLocalizer<CreatePurchaseContractDetailCommand> localizer,
            IMapper mapper
            )
        {
            _context = context;
            _localizer = localizer;
            _mapper = mapper;
        }
        public async Task<Result> Handle(CreatePurchaseContractDetailCommand request, CancellationToken cancellationToken)
        {
           //TODO:Implementing CreatePurchaseContractDetailCommandHandler method 
           var item = _mapper.Map<PurchaseContractDetail>(request);
            _context.PurchaseContractDetails.Add(item);
            await _context.SaveChangesAsync(cancellationToken);
            return  Result.Success();
        }
    }
}
