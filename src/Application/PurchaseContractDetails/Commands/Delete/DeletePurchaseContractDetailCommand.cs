using System.Linq;
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
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;

namespace CleanArchitecture.Razor.Application.PurchaseContractDetails.Commands.Delete
{
    public class DeletePurchaseContractDetailCommand: IRequest<Result>
    {
      public int Id {  get; set; }
    }
    public class DeleteCheckedPurchaseContractDetailsCommand : IRequest<Result>
    {
      public int[] Id {  get; set; }
    }

    public class DeletePurchaseContractDetailCommandHandler : 
                 IRequestHandler<DeletePurchaseContractDetailCommand, Result>,
                 IRequestHandler<DeleteCheckedPurchaseContractDetailsCommand, Result>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly IStringLocalizer<DeletePurchaseContractDetailCommandHandler> _localizer;
        public DeletePurchaseContractDetailCommandHandler(
            IApplicationDbContext context,
            IStringLocalizer<DeletePurchaseContractDetailCommandHandler> localizer,
             IMapper mapper
            )
        {
            _context = context;
            _localizer = localizer;
            _mapper = mapper;
        }
        public async Task<Result> Handle(DeletePurchaseContractDetailCommand request, CancellationToken cancellationToken)
        {
           var item = await _context.PurchaseContractDetails.FindAsync(new object[] { request.Id }, cancellationToken);
            _context.PurchaseContractDetails.Remove(item);

            var deletedEvent = new PurchaseContractDetailDeletedEvent(item);
            item.DomainEvents.Add(deletedEvent);

            await _context.SaveChangesAsync(cancellationToken);
            return Result.Success();
        }

        public async Task<Result> Handle(DeleteCheckedPurchaseContractDetailsCommand request, CancellationToken cancellationToken)
        {
            var items = await _context.PurchaseContractDetails.Where(x => request.Id.Contains(x.Id)).ToListAsync(cancellationToken);
            foreach (var item in items)
            {
                _context.PurchaseContractDetails.Remove(item);
                var deletedEvent = new PurchaseContractDetailDeletedEvent(item);
                item.DomainEvents.Add(deletedEvent);
            }
            await _context.SaveChangesAsync(cancellationToken);
            return Result.Success();
        }
    }
}
