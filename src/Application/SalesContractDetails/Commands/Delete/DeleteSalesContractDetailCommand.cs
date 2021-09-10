using System.Linq;
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
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;

namespace CleanArchitecture.Razor.Application.SalesContractDetails.Commands.Delete
{
    public class DeleteSalesContractDetailCommand: IRequest<Result>
    {
      public int Id {  get; set; }
    }
    public class DeleteCheckedSalesContractDetailsCommand : IRequest<Result>
    {
      public int[] Id {  get; set; }
    }

    public class DeleteSalesContractDetailCommandHandler : 
                 IRequestHandler<DeleteSalesContractDetailCommand, Result>,
                 IRequestHandler<DeleteCheckedSalesContractDetailsCommand, Result>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly IStringLocalizer<DeleteSalesContractDetailCommandHandler> _localizer;
        public DeleteSalesContractDetailCommandHandler(
            IApplicationDbContext context,
            IStringLocalizer<DeleteSalesContractDetailCommandHandler> localizer,
             IMapper mapper
            )
        {
            _context = context;
            _localizer = localizer;
            _mapper = mapper;
        }
        public async Task<Result> Handle(DeleteSalesContractDetailCommand request, CancellationToken cancellationToken)
        {
         
           var item = await _context.SalesContractDetails.FindAsync(new object[] { request.Id }, cancellationToken);
            _context.SalesContractDetails.Remove(item);
            await _context.SaveChangesAsync(cancellationToken);
            return Result.Success();
        }

        public async Task<Result> Handle(DeleteCheckedSalesContractDetailsCommand request, CancellationToken cancellationToken)
        {
          
           var items = await _context.SalesContractDetails.Where(x => request.Id.Contains(x.Id)).ToListAsync(cancellationToken);
            foreach (var item in items)
            {
                _context.SalesContractDetails.Remove(item);
            }
            await _context.SaveChangesAsync(cancellationToken);
            return Result.Success();
        }
    }
}
