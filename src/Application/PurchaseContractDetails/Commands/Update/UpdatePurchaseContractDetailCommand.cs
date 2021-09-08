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

namespace CleanArchitecture.Razor.Application.PurchaseContractDetails.Commands.Update
{
    public class UpdatePurchaseContractDetailCommand: PurchaseContractDetailDto,IRequest<Result>, IMapFrom<PurchaseContractDetail>
    {
        
    }

    public class UpdatePurchaseContractDetailCommandHandler : IRequestHandler<UpdatePurchaseContractDetailCommand, Result>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly IStringLocalizer<UpdatePurchaseContractDetailCommandHandler> _localizer;
        public UpdatePurchaseContractDetailCommandHandler(
            IApplicationDbContext context,
            IStringLocalizer<UpdatePurchaseContractDetailCommandHandler> localizer,
             IMapper mapper
            )
        {
            _context = context;
            _localizer = localizer;
            _mapper = mapper;
        }
        public async Task<Result> Handle(UpdatePurchaseContractDetailCommand request, CancellationToken cancellationToken)
        {
           //TODO:Implementing UpdatePurchaseContractDetailCommandHandler method 
           var item =await _context.PurchaseContractDetails.FindAsync( new object[] { request.Id }, cancellationToken);
           if (item != null)
           {
                item = _mapper.Map(request, item);
                await _context.SaveChangesAsync(cancellationToken);
           }
           return Result.Success();
        }
    }
}
