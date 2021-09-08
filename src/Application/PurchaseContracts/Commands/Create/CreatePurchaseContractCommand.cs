using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using CleanArchitecture.Razor.Application.Common.Interfaces;
using CleanArchitecture.Razor.Application.Common.Mappings;
using CleanArchitecture.Razor.Application.Common.Models;
using CleanArchitecture.Razor.Application.PurchaseContracts.DTOs;
using CleanArchitecture.Razor.Domain.Entities;
using CleanArchitecture.Razor.Domain.Events;
using MediatR;
using Microsoft.Extensions.Localization;

namespace CleanArchitecture.Razor.Application.PurchaseContracts.Commands.Create
{
    public class CreatePurchaseContractCommand: PurchaseContractDto,IRequest<Result>, IMapFrom<PurchaseContract>
    {
        public void Mapping(Profile profile)
        {
            profile.CreateMap<PurchaseContract, CreatePurchaseContractCommand>().ReverseMap();
        }
    }
    

    public class CreatePurchaseContractCommandHandler : IRequestHandler<CreatePurchaseContractCommand, Result>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly IStringLocalizer<CreatePurchaseContractCommand> _localizer;
        public CreatePurchaseContractCommandHandler(
            IApplicationDbContext context,
            IStringLocalizer<CreatePurchaseContractCommand> localizer,
            IMapper mapper
            )
        {
            _context = context;
            _localizer = localizer;
            _mapper = mapper;
        }
        public async Task<Result> Handle(CreatePurchaseContractCommand request, CancellationToken cancellationToken)
        {
           var item = _mapper.Map<PurchaseContract>(request);
            _context.PurchaseContracts.Add(item);
            await _context.SaveChangesAsync(cancellationToken);
            return  Result.Success();
        }
    }
}
