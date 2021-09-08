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

namespace CleanArchitecture.Razor.Application.PurchaseContracts.Commands.AddEdit
{
    public class AddEditPurchaseContractCommand: PurchaseContractDto,IRequest<Result>, IMapFrom<PurchaseContract>
    {
      
    }

    public class AddEditPurchaseContractCommandHandler : IRequestHandler<AddEditPurchaseContractCommand, Result>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly IStringLocalizer<AddEditPurchaseContractCommandHandler> _localizer;
        public AddEditPurchaseContractCommandHandler(
            IApplicationDbContext context,
            IStringLocalizer<AddEditPurchaseContractCommandHandler> localizer,
            IMapper mapper
            )
        {
            _context = context;
            _localizer = localizer;
            _mapper = mapper;
        }
        public async Task<Result> Handle(AddEditPurchaseContractCommand request, CancellationToken cancellationToken)
        {
            if (request.Id > 0)
            {
                var item = await _context.PurchaseContracts.FindAsync(new object[] { request.Id }, cancellationToken);
                item = _mapper.Map(request, item);
            }
            else
            {
                var item = _mapper.Map<PurchaseContract>(request);
                _context.PurchaseContracts.Add(item);
            }
            await _context.SaveChangesAsync(cancellationToken);
            return Result.Success();
        }
    }
}
