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

namespace CleanArchitecture.Razor.Application.PurchaseContracts.Commands.Update
{
    public class UpdatePurchaseContractCommand: PurchaseContractDto,IRequest<Result>, IMapFrom<PurchaseContract>
    {
        
    }

    public class UpdatePurchaseContractCommandHandler : IRequestHandler<UpdatePurchaseContractCommand, Result>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly IStringLocalizer<UpdatePurchaseContractCommandHandler> _localizer;
        public UpdatePurchaseContractCommandHandler(
            IApplicationDbContext context,
            IStringLocalizer<UpdatePurchaseContractCommandHandler> localizer,
             IMapper mapper
            )
        {
            _context = context;
            _localizer = localizer;
            _mapper = mapper;
        }
        public async Task<Result> Handle(UpdatePurchaseContractCommand request, CancellationToken cancellationToken)
        {
           var item =await _context.PurchaseContracts.FindAsync( new object[] { request.Id }, cancellationToken);
           if (item != null)
           {
                item = _mapper.Map(request, item);
                await _context.SaveChangesAsync(cancellationToken);
           }
           return Result.Success();
        }
    }
}
